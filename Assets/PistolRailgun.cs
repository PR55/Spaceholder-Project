using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRailgun : MonoBehaviour
{
    [Header("Weapon Properties")]
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public AudioSource chargeUp;
 
    public InverseSound chargeUpControls;
    public AudioSource fireEffect;

    [Header("Weapon Stats")]
    public float lineFadeTime;
    public float weaponRange = 10;
    public int damageDealt = 10;
    public float chargeSpeed = 1f;
    public float chargeTimeToFire = 2.5f;


    float chargeTime;

    bool triggerHeld;

    bool firedOnce;

    bool playingForward = false;

    // Start is called before the first frame update
    void Start()
    {
        chargeTime = 0;

        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(chargeTime < chargeTimeToFire && triggerHeld && !firedOnce)
        {
            chargeTime += Time.deltaTime * chargeSpeed;
            if (!chargeUp.isPlaying)
            {
                chargeUp.Play();
            }
        }
        else if(chargeTime > 0 && !triggerHeld)
        {
            chargeTime -= Time.deltaTime * chargeSpeed;
            if (chargeUp.isPlaying)
            {
                chargeUp.Stop();
            }
        }
        else if(chargeTime > chargeTimeToFire && triggerHeld && !firedOnce)
        {
            fireWeapon();
        }
    }

    void fireWeapon()
    {
        RaycastHit hit;

        Vector3 lastFirePosition = firePoint.position;
        
        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponRange))
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, lastFirePosition);
            lineRenderer.SetPosition(1, hit.point);
            fireEffect.Play();

            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().dealDamge(damageDealt);
            }

            firedOnce = true;

            chargeTime = 0;

            Invoke("disableLine",lineFadeTime);

        }

        
    }
    
    void disableLine()
    {
        lineRenderer.enabled = false;
    }

    public void resetFiredOnce()
    {
        if(firedOnce && !triggerHeld)
            firedOnce = false;
    }

    public void triggerPush()
    {
        triggerHeld = true;
    }
    public void triggerRelease()
    {
        triggerHeld = false;
    }


}
