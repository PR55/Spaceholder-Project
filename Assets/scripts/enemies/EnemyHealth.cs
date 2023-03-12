using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealthTotal = 50;

    [SerializeField]
    private int pointReward = 50;

    private int enemyHealth = 0;

    public AstarCustom astarAccess;

    RunReport runReport;

    private void Awake()
    {
        if (FindObjectOfType<RunReport>() != null)
            runReport = FindObjectOfType<RunReport>();

        enemyHealth = enemyHealthTotal;
    }

    public void dealDamge(int DamageDealt)
    {
        astarAccess.forceStop();
        enemyHealth -= DamageDealt;

        Debug.LogWarning("Damage Detected! Health Current: "+enemyHealth.ToString());

        if(enemyHealth <= 0)
        {
            Debug.LogWarning("Out oof Health!");
            if(runReport != null)
            {
                runReport.addToScore(pointReward);
            }
            astarAccess.forceStop();
        }
    }
}
