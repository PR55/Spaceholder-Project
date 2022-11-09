using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIChangeTarget : MonoBehaviour
{

    public Transform target;

    public float checkWait = 1f;

    AIPath aIPathFinder;

    PatrolAttributes patrolAttributes;

    Transform player;

    bool stopped = false;
    // Start is called before the first frame update
    void Awake()
    {
        aIPathFinder = GetComponent<AIPath>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(target !=null)
        {
            GetComponent<AIDestinationSetter>().target = target;
        }
    }


    private void FixedUpdate()
    {
        
    }


    public void patrolAttribute(PatrolAttributes pointHolder)
    {
        patrolAttributes = pointHolder;
    }




}
