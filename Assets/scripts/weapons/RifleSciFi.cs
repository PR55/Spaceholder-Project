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

    public Quaternion rotationModifier = Quaternion.identity;

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
            audioSource.Stop();

        audioSource.Play();

        BulletStandard spawnedProjectile = Instantiate(bullet, firePoint.position, new Quaternion(firePoint.rotation.x + rotationModifier.x, firePoint.rotation.y + rotationModifier.y, firePoint.rotation.z + rotationModifier.z, 1)).GetComponent<BulletStandard>();

        spawnedProjectile.ColliderstoIgnore(objectColliders);

        spawnedProjectile.firePoint(firePoint);

        if(fullAuto)
        {
            fireCooldown = fireRate + Time.fixedDeltaTime;
        }
    }

}
