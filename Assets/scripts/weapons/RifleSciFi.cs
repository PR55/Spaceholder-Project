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
    [SerializeField]
    private MeshCollider[] objectColliders;

    bool fullAuto = false;

    float fireCooldown = 0;

    public AudioSource audioSource;

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

    public void FireBullet()
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

        if(fullAuto)
        {
            fireCooldown = fireRate + Time.fixedDeltaTime;
        }
    }

}
