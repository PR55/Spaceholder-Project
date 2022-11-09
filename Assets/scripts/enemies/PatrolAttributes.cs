using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolAttributes : MonoBehaviour
{
    public GameObject[] patrolPoints;
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    private GameObject enemySpawned;

    int index = 0;

    [SerializeField]
    private bool testing = false;

    private void Awake()
    {
        if(testing)
        {
            EnemySpawn();
        }
    }
    public void EnemySpawn()
    {
        enemySpawned = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        if(enemySpawned.GetComponent<AstarCustom>() != null)
        {
            enemySpawned.GetComponent<AstarCustom>().patrolAttribute(this);
        }

        enemySpawned.GetComponent<AIDestinationSetter>().target = patrolPoints[index].transform;
    }

    public void nextIndex()
    {
        index++;
        if (index >= patrolPoints.Length)
        {
            index = 0;
        }
        enemySpawned.GetComponent<AIDestinationSetter>().target = patrolPoints[index].transform;
    }

    public void ResumeMove()
    {
        enemySpawned.GetComponent<AIDestinationSetter>().target = patrolPoints[index].transform;
    }

    public void StopMove()
    {
        enemySpawned.GetComponent<AIDestinationSetter>().target = enemySpawned.transform;
    }

    public void chasePlayer(Transform player)
    {
        enemySpawned.GetComponent<AIDestinationSetter>().target = player;
    }

    public void roomDestroy()
    {
        enemySpawned.GetComponent<AstarCustom>().forceStop();
    }

}
