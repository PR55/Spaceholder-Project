using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealthTotal = 50;

    private int enemyHealth = 0;

    AstarCustom astarAccess;

    private void Awake()
    {
        enemyHealth = enemyHealthTotal;
        astarAccess = this.gameObject.GetComponent<AstarCustom>();
    }

    public void dealDamge(int DamageDealt)
    {
        enemyHealth -= DamageDealt;

        if(enemyHealth <= 0)
        {
            astarAccess.StopAllCoroutines();
            astarAccess.forceStop();
        }
    }
}
