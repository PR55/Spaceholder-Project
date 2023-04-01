using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccessNextLevel : MonoBehaviour
{
    public float activateDistance = 3;
    public GameObject parent;

    public bool overrideLevel;

    RoomGeneration roomGeneration;
    GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        roomGeneration = FindObjectOfType<RoomGeneration>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(overrideLevel)
        {
            player.transform.position = this.transform.position + new Vector3(0, 0, -5);
            overrideLevelNow();
        }
    }

    public void NextLevel()
    {
        if(parent != null && Vector3.Distance(this.gameObject.transform.position, player.transform.position) < activateDistance && roomGeneration.EndRoomCurrent() == parent)
        {
            
            if(roomGeneration != null)
            roomGeneration.NextLevel(roomGeneration.EndRoomCurrent());

            Destroy(this.gameObject);
        }
    }

    void overrideLevelNow()
    {
        if (roomGeneration != null)
            roomGeneration.NextLevel(roomGeneration.EndRoomCurrent());
        Destroy(this.gameObject);
    }

}
