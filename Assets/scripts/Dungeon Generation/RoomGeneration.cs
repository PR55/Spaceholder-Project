using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomGeneration : MonoBehaviour
{
    [Header("Other Scripts")]
    public GridSpawn gridSpawn;
    public HallwayGeneration hallwayGeneration;

    //if (i == Mathf.FloorToInt(spawnedRooms.Length / 2)) half to guarantee end room spawn

    [Header("Current Attributes")]
    [SerializeField]
    private GameObject startRoom;
    [Header("Prefabs")]
    [SerializeField]
    private GameObject endRoomPrefab;
    [SerializeField]
    private GameObject[] roomChoices;

    [Header("Attributes")]
    [SerializeField, Tooltip("Starting amount of Rooms to build")]
    private int amountWanted = 3;
    [SerializeField]
    private int maximumIncreaseRate = 3;
    [SerializeField]
    private int maxAllowedRooms = 10;
    [SerializeField]
    private Transform roomParent;
    [SerializeField]
    private Transform hallwayParent;

    GameObject[] spawnedRooms;
    GameObject builtEndRoom;

    [Header("Test Activation")]
    [SerializeField]
    private bool testing;

    GameObject[] spawnedPoints;
    List<GameObject> spawnedPointsChoice = new List<GameObject>();
    List<pointProperties> doorways = new List<pointProperties>();
    public void Start()
    {
        if(testing)
            GenerateRooms();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            NextLevel(builtEndRoom);
        }
    }

    public void GenerateRooms()
    {
        gridSpawn.SetOrigin(startRoom.transform.position);
        gridSpawn.SpawnGrid();
        spawnedRooms = new GameObject[amountWanted + 2];
        spawnedRooms[0] = startRoom;
        foreach (pointProperties a in spawnedRooms[0].GetComponent<RoomAttribute>().Doorways())
        {
            if (!doorways.Contains(a))
            {
                doorways.Add(a);
            }
        }
        for (int i = 0; i < amountWanted + 1;)
        {
            UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
            int randomIndex = UnityEngine.Random.Range(1, gridSpawn.GridPoints().Length);
            if(!spawnedPointsChoice.Contains(gridSpawn.GridPoints()[randomIndex]))
            {
                spawnedPointsChoice.Add(gridSpawn.GridPoints()[randomIndex]);
                i++;
            }
        }

        spawnedPoints = new GameObject[spawnedPointsChoice.Count];
        spawnedPoints = spawnedPointsChoice.ToArray();
        int j = 1;
        while(j < spawnedRooms.Length)
        {
            if (j == Mathf.FloorToInt(spawnedRooms.Length / 2) && builtEndRoom == null) //if
            {
                UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
                int randomIndex = UnityEngine.Random.Range(0, spawnedPoints.Length);
                if(!spawnedPoints[randomIndex].GetComponent<GridPoint>().useCheck())
                {
                    spawnedRooms[j] = Instantiate(endRoomPrefab, spawnedPoints[randomIndex].transform.position, Quaternion.identity);
                    foreach (pointProperties a in spawnedRooms[j].GetComponent<RoomAttribute>().Doorways())
                    {
                        if (!doorways.Contains(a))
                        {
                            doorways.Add(a);
                        }
                    }
                    builtEndRoom = spawnedRooms[j];
                    spawnedPoints[randomIndex].GetComponent<GridPoint>().hasUsed();
                    j++;
                }
                
            }
            else
            {
                UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
                int randomIndex = UnityEngine.Random.Range(0, spawnedPoints.Length);
                int randomIndex2 = UnityEngine.Random.Range(0, roomChoices.Length);
                if (!spawnedPoints[randomIndex].GetComponent<GridPoint>().useCheck())
                {
                    spawnedRooms[j] = Instantiate(roomChoices[randomIndex2], spawnedPoints[randomIndex].transform.position, Quaternion.identity);
                    spawnedRooms[j].transform.parent = roomParent; 
                    foreach(pointProperties a in spawnedRooms[j].GetComponent<RoomAttribute>().Doorways())
                    {
                        if(!doorways.Contains(a))
                        {
                            doorways.Add(a);
                        }
                    }
                    spawnedPoints[randomIndex].GetComponent<GridPoint>().hasUsed();
                    j++;
                }
                
            }

            
        }

        pointProperties[] doorway = new pointProperties[doorways.Count];
        doorway = doorways.ToArray();
        GameObject closestRoom = null;
        int direction = 4;

        foreach(GameObject a in spawnedRooms)
        {
            foreach(GameObject b in spawnedRooms)
            {
                // Check for closest room, then check points to see if used
                if(closestRoom!= null && Vector3.Distance(a.transform.position, b.transform.position) < Vector3.Distance(a.transform.position, closestRoom.transform.position))
                {
                    if(!a.GetComponent<RoomAttribute>().Doorways()[0].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[1].useCheck())
                    {
                        if(a.transform.position.x == b.transform.position.x && a.transform.position.z < b.transform.position.z)
                        {
                            closestRoom = b;
                            direction = 0;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[1].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[0].useCheck())
                    {
                        if (a.transform.position.x == b.transform.position.x && a.transform.position.z > b.transform.position.z)
                        {
                            closestRoom = b;
                            direction = 1;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[2].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[3].useCheck())
                    {
                        if (a.transform.position.z == b.transform.position.z && a.transform.position.x < b.transform.position.x)
                        {
                            closestRoom = b;
                            direction = 2;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[3].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[2].useCheck())
                    {
                        if (a.transform.position.z == b.transform.position.z && a.transform.position.x > b.transform.position.x)
                        {
                            closestRoom = b;
                            direction = 3;
                        }
                    }
                }
                else
                {
                    if (!a.GetComponent<RoomAttribute>().Doorways()[0].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[1].useCheck())
                    {
                        if (a.transform.position.x == b.transform.position.x && a.transform.position.z < b.transform.position.z)
                        {
                            closestRoom = b;
                            direction = 0;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[1].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[0].useCheck())
                    {
                        if (a.transform.position.x == b.transform.position.x && a.transform.position.z > b.transform.position.z)
                        {
                            closestRoom = b;
                            direction = 1;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[2].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[3].useCheck())
                    {
                        if (a.transform.position.z == b.transform.position.z && a.transform.position.x < b.transform.position.x)
                        {
                            closestRoom = b;
                            direction = 2;
                        }
                    }
                    else if (!a.GetComponent<RoomAttribute>().Doorways()[3].useCheck() && !b.GetComponent<RoomAttribute>().Doorways()[2].useCheck())
                    {
                        if (a.transform.position.z == b.transform.position.z && a.transform.position.x > b.transform.position.x)
                        {
                            closestRoom = b;
                            direction = 3;
                        }
                    }
                }
            }
            if (closestRoom != null)
            {
                if (direction == 0)
                {
                    hallwayGeneration.CornerPoint(a.GetComponent<RoomAttribute>().Doorways()[0].gameObject, closestRoom.GetComponent<RoomAttribute>().Doorways()[1].gameObject, true);
                }
                else if (direction == 1)
                {
                    hallwayGeneration.CornerPoint(a.GetComponent<RoomAttribute>().Doorways()[1].gameObject, closestRoom.GetComponent<RoomAttribute>().Doorways()[0].gameObject, true);
                }
                else if (direction == 2)
                {
                    hallwayGeneration.CornerPoint(a.GetComponent<RoomAttribute>().Doorways()[2].gameObject, closestRoom.GetComponent<RoomAttribute>().Doorways()[3].gameObject, false);
                }
                else if (direction == 3)
                {
                    hallwayGeneration.CornerPoint(a.GetComponent<RoomAttribute>().Doorways()[3].gameObject, closestRoom.GetComponent<RoomAttribute>().Doorways()[2].gameObject, false);
                }
                direction = 4;
                closestRoom = null;

            }
        }
        
        foreach (pointProperties a in doorway)
        {
            a.doorChAct();
        }

        gridSpawn.GridDestroy();

        Resources.UnloadUnusedAssets();

    }

    public void NextLevel(GameObject endRoom)
    {
        if(endRoom != startRoom)
        {
            startRoom = endRoom;

            foreach(GameObject room in spawnedRooms)
            {
                if(room != startRoom)
                {
                    Destroy(room);
                }
            }
            foreach(FloorBehaviour floor in GameObject.FindObjectsOfType<FloorBehaviour>())
            {
                Destroy(floor.gameObject);
            }
            foreach(pointProperties doors in startRoom.GetComponent<RoomAttribute>().Doorways())
            {
                doors.pointReset();
                doors.doorReset();
            }
            spawnedPoints = null;
            doorways.Clear();
            Resources.UnloadUnusedAssets();
            GenerateRooms();
        }
    }
}
