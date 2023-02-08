using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RoomAttribute : MonoBehaviour
{
    public pointProperties[] doorways;

    public PatrolAttributes patrolController;

    [SerializeField]
    private float spaceTakenx = 3;
    [SerializeField]
    private float spaceTakenz = 3;
    [SerializeField]
    private bool isStart = false;


    public Vector2 RoomDimensions()
    {
        Vector2 roomSize = new Vector2(spaceTakenx, spaceTakenz);
        return roomSize;
    }

    public pointProperties[] Doorways()
    {
        return doorways;
    }

    public void SpawnEnemy()
    {
        if(patrolController != null)
            patrolController.EnemySpawn();
    }

    public void killEnemies()
    {
        patrolController.roomDestroy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.gameObject.transform.position.x + (spaceTakenx / 4), this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z + (spaceTakenz / 4)), new Vector3(spaceTakenx / 2, 2, spaceTakenz / 2));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(this.gameObject.transform.position.x + (spaceTakenx / -4), this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z + (spaceTakenz / 4)), new Vector3(spaceTakenx / 2, 2, spaceTakenz / 2));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(this.gameObject.transform.position.x + (spaceTakenx / -4), this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z + (spaceTakenz / -4)), new Vector3(spaceTakenx / 2, 2, spaceTakenz / 2));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(this.gameObject.transform.position.x + (spaceTakenx / 4), this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z + (spaceTakenz / -4)), new Vector3(spaceTakenx / 2, 2, spaceTakenz / 2));
    }

}
