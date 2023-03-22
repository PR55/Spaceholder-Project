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
        enemyHealth = enemyHealthTotal;
    }
    public void Start()
    {
        runReport = FindObjectOfType<RunChecker>().runTracker();
    }
    public void dealDamge(int DamageDealt)
    {
        enemyHealth -= DamageDealt;

        Debug.LogWarning("Damage Detected! Health Current: "+enemyHealth.ToString());

        if(enemyHealth <= 0)
        {
            Debug.LogWarning("Out oof Health!");
            GameObject.FindGameObjectWithTag("Reporter").GetComponent<RunReport>().addToScore(pointReward);
            astarAccess.forceStop();
        }
    }
}
