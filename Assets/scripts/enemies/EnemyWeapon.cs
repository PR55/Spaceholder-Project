using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject enemyBullet;

    public Transform firePoint;

    public LineRenderer lineRendererFire;

    public float intervalDeactivate = 1f;

    public float fireWait = .5f;

    float fireTimer = 0f;

    private void Awake()
    {
        deactivateLine();
    }

    private void FixedUpdate()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.fixedDeltaTime;
        }
    }

    public void fireWeapon(LayerMask player)
    {
        if(fireTimer <= 0)
        {
            Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
            fireTimer = fireWait + Time.fixedDeltaTime;
        }
        

    }

    public void lookAtPlayer(Transform player)
    {
        this.transform.LookAt(player);
    }

    void deactivateLine()
    {
        lineRendererFire.enabled = false;
    }

}
