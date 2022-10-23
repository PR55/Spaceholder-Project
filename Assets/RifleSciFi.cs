using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    bool fullAuto = false;

    float fireCooldown = 0;
    BulletStandard bulletStandard;
    // Start is called before the first frame update
    void Start()
    {
        bulletStandard = GetComponent<BulletStandard>();
    }

    private void FixedUpdate()
    {
        if(fireCooldown >= 0)
        {
            fireCooldown -= Time.fixedDeltaTime * 1000;
        }
    }

    private void Update()
    {
        if(!isVR)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (fullAuto)
                {
                    fullAuto = false;
                }
                else
                {
                    fullAuto = true;
                }
            }

            if (fullAuto)
            {
                if (Input.GetMouseButton(0) && fireCooldown! > .5)
                {
                    FireBullet();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    FireBullet();
                }
            }
        }
    }


    public void FireBullet()
    {
        BulletStandard spawnedProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<BulletStandard>();

        spawnedProjectile.firePoint(firePoint);

        if(fullAuto)
        {
            fireCooldown = fireRate + Time.fixedDeltaTime;
        }
    }

}
