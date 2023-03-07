using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;
using UnityEngine.SceneManagement;
public class AstarCustom : AIPath
{
    
    public enum State
    {
        STOP,
        PATROL,
        INVESTIGATE,
        CHASE,
        ATTACK,
        DEAD
    }
    //Patrol Properties
    bool patrol = true;
    bool playerSpotted = false;
    bool investigate = false;
    private State currentState = State.PATROL;
    public float checkWait = 1f;
    public Transform visualBody;
    public Animator enemyAnimation;

    //weapon Properties
    public EnemyWeapon enemyWeapon;
    public LookTest weaponLook;
    public LookTest weaponPoint;

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

    //Death Properties
    public float totalDeathWait = 2f;

    //Misc. Properties
    PatrolAttributes patrolAttributes;
    public Transform player;

    //loot properties
    public GameObject[] lootItems;
    LootTable lootTable;

    private void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStatsTracker>().curPlayer().playerTarget();

        lootTable = FindObjectOfType<LootTable>();

        if(weaponPoint != null)
        {
            weaponPoint.target = player;
            weaponPoint.targetFound = true;
            weaponLook.target = player;
        }

        currentState = State.PATROL;

        if (visualBody == null)
            visualBody = this.transform;
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
            if(weaponLook != null && weaponLook.targetFound)
            {
                weaponLook.targetFound = false;
            }
            if(enemyAnimation != null && !enemyAnimation.GetBool("isWalking"))
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
                if (enemyAnimation != null && enemyAnimation.GetBool("isWalking"))
                {
                    enemyAnimation.SetBool("isWalking", false);
                }
                if (weaponLook != null && !weaponLook.targetFound)
                {
                    weaponLook.targetFound = true;
                }
                enemyWeapon.fireWeapon(targetMask);
            }
            else
            {
                if (enemyAnimation != null && !enemyAnimation.GetBool("isWalking"))
                {
                    enemyAnimation.SetBool("isWalking", true);
                }
                if (weaponLook != null && weaponLook.targetFound)
                {
                    weaponLook.targetFound = false;
                }
                base.Update();
            }
        }
        else if(currentState == State.DEAD)
        {
            if (enemyAnimation.GetBool("Dead") == false)
                enemyAnimation.SetBool("Dead", true);

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
                currentState = State.DEAD;
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

    private void OnDestroy()
    {
        if(SceneManager.GetActiveScene().isLoaded)
        {
            Vector3 enemyPos = transform.position + new Vector3(0, 1.5f, 0);
            foreach (GameObject loot in lootItems)
            {
                if (lootTable.canSpawn(loot))
                {
                    Instantiate(loot, enemyPos, loot.transform.rotation);
                    lootTable.setSpawnState(loot, true);
                }
            }
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

    private void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        UnityEditor.Handles.color = Color.white;

        UnityEditor.Handles.DrawWireArc(viewPoint.transform.position,Vector3.up,Vector3.forward,360,radiusSight);

        Vector3 viewAngle01 = DirectionFromAngle(viewPoint.transform.eulerAngles.y, -angle/2);
        Vector3 viewAngle02 = DirectionFromAngle(viewPoint.transform.eulerAngles.y, angle / 2);

        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawLine(viewPoint.transform.position, viewPoint.transform.position+viewAngle01*radiusSight);
        UnityEditor.Handles.DrawLine(viewPoint.transform.position, viewPoint.transform.position + viewAngle02 * radiusSight);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
