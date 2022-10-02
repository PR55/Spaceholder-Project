using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generaterooms : MonoBehaviour
{

    public Vector2 range = new Vector2(20, 20);

    public GameObject[] roomsChoice;

    public int totalAmountAllowed = 5;

    public float minSpacing = 5f;

    public GameObject spawnMarkerObject;

    Transform spawnMarker;

    GameObject[] roomsActive;
    GameObject[] roomsActiveSpawn;

    bool isCollide = false;

    public List<pointProperties> allDoorWays;
    public pointProperties[] pointsDoorways;

    // Start is called before the first frame update
    private void Start()
    {
        spawnMarker = Instantiate(spawnMarkerObject).transform;
        roomsActive = new GameObject[totalAmountAllowed + 2];
        roomsActiveSpawn = new GameObject[totalAmountAllowed];
        roomsActive[0] = GameObject.Find("Start Room");
        roomsActive[1] = GameObject.Find("End Room");



    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateRooms();
        }
    }

    public void GenerateRooms()
    {
        ClearAll();
        UnityEngine.Random.seed = DateTime.UtcNow.Millisecond;
        int i = 0;
        while(i < totalAmountAllowed)
        {
            spawnMarker.position = new Vector3(UnityEngine.Random.Range(-range.x/2, range.x/2), 0, UnityEngine.Random.Range(-range.y/2, range.y/2));
            var RoomChoice = roomsChoice[UnityEngine.Random.Range(0, roomsChoice.Length)];
            foreach (GameObject room in roomsActive)
            {
                if(room != null)
                {
                    if(Vector3.Distance(new Vector3(0,0,spawnMarker.position.z),new Vector3(0,0,room.GetComponent<RoomAttribute>().RoomDimensions().y)) >= (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().y)+minSpacing || Vector3.Distance(new Vector3(spawnMarker.position.x,0,0), new Vector3(room.GetComponent<RoomAttribute>().RoomDimensions().x, 0, 0)) >= (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().x) + minSpacing)
                    {
                        isCollide = false;
                    }
                    else
                    {
                        isCollide = true;
                        break;
                    }
                }
            }

            if(isCollide)
            { }
            else
            {
                var newRoom = Instantiate(RoomChoice, new Vector3(spawnMarker.position.x, spawnMarker.position.y, spawnMarker.position.z), spawnMarker.rotation);
                newRoom.transform.parent = this.gameObject.transform;
                roomsActive[i + 2] = newRoom;
                roomsActiveSpawn[i] = newRoom;
                foreach(pointProperties a in newRoom.GetComponent<RoomAttribute>().Doorways())
                {
                    if(!allDoorWays.Contains(a))
                    {
                        allDoorWays.Add(a);
                    }
                }
                
                i++;
            }
            isCollide = false;

        }
        pointsDoorways = new pointProperties[allDoorWays.Count];
        pointsDoorways = allDoorWays.ToArray();
    }

    void ClearAll()
    {
        foreach(GameObject room in roomsActiveSpawn)
        {
            if(room != null)
            {
                Destroy(room);
            }
        }
        allDoorWays.Clear();
        pointsDoorways = null;
        roomsActive = new GameObject[totalAmountAllowed + 2];
        roomsActiveSpawn = new GameObject[totalAmountAllowed];
        roomsActive[0] = GameObject.Find("Start Room");
        roomsActive[1] = GameObject.Find("End Room");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(range.x / 4, this.gameObject.transform.position.y + 1, range.y / 4), new Vector3(range.x/2,2,range.y/2));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(range.x / -4, this.gameObject.transform.position.y + 1, range.y / 4), new Vector3(range.x / 2, 2, range.y / 2));
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(range.x / -4, this.gameObject.transform.position.y + 1, range.y / -4), new Vector3(range.x / 2, 2, range.y / 2));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(range.x / 4, this.gameObject.transform.position.y + 1, range.y / -4), new Vector3(range.x / 2, 2, range.y / 2));
        
    }
}
