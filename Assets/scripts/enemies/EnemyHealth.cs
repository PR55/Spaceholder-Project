using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealthTotal = 50;

    public float alphaShow;

    public SkinnedMeshRenderer damageShown;

    public float decaySpeed;

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

    private void FixedUpdate()
    {
        if(damageShown.materials[damageShown.materials .Length- 1].GetColor("_DamageColor").a > 0)
        {
            foreach (Material a in damageShown.materials)
            {
                Color color = a.GetColor("_DamageColor");
                color.a = color.a - (Time.fixedDeltaTime * decaySpeed);
                a.SetColor("_DamageColor", color);
            }
        }
    }

    public void dealDamge(int DamageDealt)
    {
        if(!astarAccess.CheckDeath())
        {
            enemyHealth -= DamageDealt;

            foreach  (Material a in damageShown.materials)
            {
                Color color = a.GetColor("_DamageColor");
                color.a = alphaShow;
                a.SetColor("_DamageColor", color);
            }

            Debug.LogWarning("Damage Detected! Health Current: " + enemyHealth.ToString());

            if (enemyHealth <= 0)
            {
                Debug.LogWarning("Out oof Health!");
                GameObject.FindGameObjectWithTag("Reporter").GetComponent<RunReport>().addToScore(pointReward);
                astarAccess.forceStop();
            }
            else
            {
                astarAccess.alertEnemy();
            }
        }
        
    }
}
