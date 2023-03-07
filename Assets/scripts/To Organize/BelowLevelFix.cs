using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelowLevelFix : MonoBehaviour
{
    [Header("Positions/Offsets")]
    public Vector3 triggerPosition;
    public Vector3 respawnOffset;
    [Header("Dungeon Level")]
    public RoomGeneration roomGeneration;
    [Header("Main and Level Report")]
    public GameObject mainRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < triggerPosition.y)
        {
            if(roomGeneration != null)
            {
                transform.position = roomGeneration.firstRoom().transform.position + respawnOffset;
            }
            else if(mainRoom != null)
            {
                transform.position = mainRoom.transform.position + respawnOffset;
            }
        }
    }
}
