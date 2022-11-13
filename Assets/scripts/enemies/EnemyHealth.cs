using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealthTotal = 50;

    [SerializeField]
    private int pointReward = 50;

    private int enemyHealth = 0;

    AstarCustom astarAccess;

    RunReport runReport;

    private void Awake()
    {
        if (FindObjectOfType<RunReport>() != null)
            runReport = FindObjectOfType<RunReport>();

        enemyHealth = enemyHealthTotal;
        astarAccess = this.gameObject.GetComponent<AstarCustom>();
    }

    public void dealDamge(int DamageDealt)
    {
        enemyHealth -= DamageDealt;

        if(enemyHealth <= 0)
        {
            if(runReport != null)
            {
                runReport.addToScore(pointReward);
            }
            astarAccess.StopAllCoroutines();
            astarAccess.forceStop();
        }
    }
}
