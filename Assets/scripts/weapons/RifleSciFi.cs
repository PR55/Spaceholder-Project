using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RifleSciFi : MonoBehaviour
{

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 0f;
    [SerializeField]
    private bool isVR = true;
    [SerializeField]
    private MeshCollider[] objectColliders;

    bool fullAuto = false;

    float fireCooldown = 0;

    public AudioSource audioSource;

    BulletStandard bulletStandard;

    public bool useMag;

    Magazine magazine;

    public XRSocketInteractor socketInteractor;

    public void submitMag(XRBaseInteractable interactable)
    {
        if (interactable.GetComponent<Magazine>() != null)
        {
            magazine = interactable.GetComponent<Magazine>();
        }
    }

    public void removeMag(XRBaseInteractable interactable)
    {
        magazine = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletStandard = GetComponent<BulletStandard>();

        if(useMag)
        {
            socketInteractor.onSelectEntered.AddListener(submitMag);
            socketInteractor.onSelectExited.AddListener(removeMag);
        }
        
    }

    private void FixedUpdate()
    {
        if(fireCooldown >= 0)
        {
            fireCooldown -= Time.fixedDeltaTime * .5f;
        }
    }



    public void FireBullet()
    {
        if (useMag)
        {
            if (magazine != null)
            {
                bool test = magazine.shotFired();
                if (test)
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                        audioSource.time = 0;
                    }

                    audioSource.Play();

                    BulletStandard spawnedProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<BulletStandard>();

                    spawnedProjectile.ColliderstoIgnore(objectColliders);

                    spawnedProjectile.firePoint(firePoint);

                    if (fullAuto)
                    {
                        fireCooldown = fireRate + Time.fixedDeltaTime;
                    }
                }
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.time = 0;
            }

            audioSource.Play();

            BulletStandard spawnedProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<BulletStandard>();

            spawnedProjectile.ColliderstoIgnore(objectColliders);

            spawnedProjectile.firePoint(firePoint);

            if (fullAuto)
            {
                fireCooldown = fireRate + Time.fixedDeltaTime;
            }
        }
        
    }

}
