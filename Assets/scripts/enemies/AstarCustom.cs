using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AstarCustom : AIPath
{
    
    public enum State
    {
        STOP,
        PATROL,
        INVESTIGATE,
        CHASE,
        ATTACK
    }
    //Patrol Properties
    bool patrol = true;
    bool playerSpotted = false;
    bool investigate = false;
    private State currentState = State.PATROL;
    public float checkWait = 1f;
    public Transform visualBody;

    public LookTest weaponLook;
    public LookTest weaponPoint;

    public Animator enemyAnimation;

    //weapon Properties
    public EnemyWeapon enemyWeapon;


    //Sight Properties
    public GameObject viewPoint;
    public float sightTimer = .2f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float radiusSight;
    [Range(0, 360)]
    public float angle;
    bool canSeePlayer;

    public float endReachPatrol;
    public float endReachChase;

    //Misc. Properties
    PatrolAttributes patrolAttributes;
    public Transform player;
    


    private void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStatsTracker>().curPlayer().playerTarget();

        weaponPoint.target = player;
        weaponPoint.targetFound = true;
        weaponLook.target = player;


        StartCoroutine(FOVRoutine());
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();

    }
    private void Update()
    {
        if(currentState == State.PATROL)
        {
            if(weaponLook.targetFound)
            {
                weaponLook.targetFound = false;
            }
            if(!enemyAnimation.GetBool("isWalking"))
            {
                enemyAnimation.SetBool("isWalking", true);
            }
            base.Update();
            if (reachedDestination)
            {
                patrolAttributes.nextIndex();
            }
        }
        else if(currentState == State.CHASE)
        {
            if(reachedDestination)
            {
                if (enemyAnimation.GetBool("isWalking"))
                {
                    enemyAnimation.SetBool("isWalking", false);
                }
                if (!weaponLook.targetFound)
                {
                    weaponLook.targetFound = true;
                }
                enemyWeapon.fireWeapon(targetMask);
            }
            else
            {
                if (!enemyAnimation.GetBool("isWalking"))
                {
                    enemyAnimation.SetBool("isWalking", true);
                }
                base.Update();
            }
        }
        else if (currentState == State.STOP)
        {
            if (patrol)
            {
                currentState = State.PATROL;
                endReachedDistance = endReachPatrol;
                patrolAttributes.ResumeMove();

            }
            else if (playerSpotted)
            {
                currentState = State.CHASE;
                endReachedDistance = endReachChase;
                patrolAttributes.chasePlayer(player);
            }
            else if(!patrol && !playerSpotted && !investigate)
            {
                Destroy(this.gameObject);
            }
        }
        if(canSeePlayer && !playerSpotted)
        {
            currentState = State.STOP;
            patrol = false;
            playerSpotted = true;
        }
        else if(!canSeePlayer && !patrol)
        {
            currentState = State.STOP;
            patrol = true;
            playerSpotted = false;
        }
        

    }
    public void patrolAttribute(PatrolAttributes pointHolder)
    {
        patrolAttributes = pointHolder;
    }

    public void forceStop()
    {
        StopAllCoroutines();
        patrol = false;
        playerSpotted = false;
        investigate = false;
        currentState = State.STOP;
    }

    private IEnumerator FOVRoutine()
    {
        while(true)
        {
            yield return sightTimer;
            FieldOfViewCheck();
        }

    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(visualBody.transform.position, radiusSight, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - visualBody.transform.position).normalized;

            if (Vector3.Angle(visualBody.transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(visualBody.transform.position, target.position);

                if (!Physics.Raycast(visualBody.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

}
