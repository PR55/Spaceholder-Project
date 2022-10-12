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

    GameObject[] hallways;

    bool isCollide = false;

    public List<pointProperties> allDoorWays;
    public pointProperties[] pointsDoorways;
    public HallwayGeneration hallwayGeneration;

    // Start is called before the first frame update
    private void Start()
    {
        spawnMarker = Instantiate(spawnMarkerObject).transform;
        roomsActive = new GameObject[totalAmountAllowed + 2];
        roomsActiveSpawn = new GameObject[totalAmountAllowed];
        roomsActive[0] = GameObject.Find("Start Room");
        roomsActive[1] = GameObject.Find("End Room");
        foreach (GameObject a in roomsActive)
        {
            if (a != null && a.GetComponent<RoomAttribute>() != null)
            {
                foreach (pointProperties b in a.GetComponent<RoomAttribute>().Doorways())
                {
                    if (allDoorWays.Contains(b) != true)
                    {
                        allDoorWays.Add(b);
                    }
                }
            }
        }
    }
    public void GenerateRooms()
    {
        ClearAlls();
        hallwayGeneration.ClearAll();
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
                    if((((spawnMarker.position.z + (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().y/2)) > ((room.transform.position.z + (room.GetComponent<RoomAttribute>().RoomDimensions().y/2)) + minSpacing) || ((spawnMarker.position.z - (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().y / 2)) < (room.transform.position.z - (room.GetComponent<RoomAttribute>().RoomDimensions().y / 2)) + minSpacing)) && ((spawnMarker.position.x + (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().x / 2)) > (room.transform.position.x + (room.GetComponent<RoomAttribute>().RoomDimensions().x / 2)) + minSpacing)) || ((spawnMarker.position.x - (RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().x / 2) < (room.transform.position.x - (room.GetComponent<RoomAttribute>().RoomDimensions().x / 2)) + minSpacing)))
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
            {
                //Debug.Log("Collision");
                //i++;
            }
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
        pointProperties closestPoint = null;
        foreach (pointProperties a in pointsDoorways)
        {
            if (!a.useCheck())
            {
                foreach (pointProperties b in pointsDoorways)
                {
                    if (b != a)
                    {
                        if (!b.useCheck())
                        {
                            if (b.parentCheck().transform != a.parentCheck().transform)
                            {
                                if (closestPoint != null)
                                {
                                    if (Vector3.Distance(a.gameObject.transform.position, b.gameObject.transform.position) < Vector3.Distance(a.gameObject.transform.position, closestPoint.gameObject.transform.position))
                                    {
                                        closestPoint = b;
                                    }
                                }
                                else
                                {
                                    closestPoint = b;
                                }
                            }
                        }
                    }
                }
                if(closestPoint != null)
                hallwayGeneration.pointCheck(a.gameObject, closestPoint.gameObject);

                closestPoint = null;
            }
        }
        foreach (pointProperties a in pointsDoorways)
        {
            a.doorChAct();
        }
        hallways = null;
        hallways = new GameObject[hallwayGeneration.hallwayColelction().Length];
        hallways = hallwayGeneration.hallwayColelction();

        Resources.UnloadUnusedAssets();
    }

    void ClearAlls()
    {
        roomsActiveSpawn = new GameObject[GameObject.FindObjectsOfType<RoomAttribute>().Length];

        int v = 0;

        foreach(RoomAttribute a in GameObject.FindObjectsOfType<RoomAttribute>())
        {
            if(a.gameObject.name != "Start Room" && a.gameObject.name != "End Room")
            {
                roomsActiveSpawn[v] = a.gameObject;
            }
            v++;
        }
        foreach(GameObject room in roomsActiveSpawn)
        {
            if(room != null)
            {
                Destroy(room);
            }
        }
        if(hallways != null)
        {
            foreach (GameObject hall in hallways)
            {
                if (hall != null)
                {
                    Destroy(hall);
                }
            }
        }
        
        allDoorWays.Clear();
        pointsDoorways = null;
        roomsActive = new GameObject[totalAmountAllowed + 2];
        roomsActiveSpawn = new GameObject[totalAmountAllowed];
        roomsActive[0] = GameObject.Find("Start Room");
        roomsActive[1] = GameObject.Find("End Room");
        foreach(GameObject a in roomsActive)
        {
            if(a != null && a.GetComponent<RoomAttribute>() != null)
            {
                foreach (pointProperties b in a.GetComponent<RoomAttribute>().Doorways())
                {
                    if(allDoorWays.Contains(b) != true)
                    {
                        b.pointReset();
                        allDoorWays.Add(b);
                    }
                }
            }
        }
        pointsDoorways = null;
        hallways = null;
        Resources.UnloadUnusedAssets();
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
