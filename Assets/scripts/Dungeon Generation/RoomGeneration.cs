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
    [SerializeField]
    private AstarPath astarPath;
    //if (i == Mathf.FloorToInt(spawnedRooms.Length / 2)) half to guarantee end room spawn

    [Header("Current Attributes")]
    public GameObject startRoomPrefab;
    private GameObject startRoom;
    [Header("Prefabs")]
    [SerializeField]
    private GameObject endRoomPrefab;
    [SerializeField]
    private GameObject[] roomChoices;

    [Header("Attributes")]
    [SerializeField, Tooltip("Starting amount of Rooms to build.\n Determined by Spawn Grid Size:\n y = (gridX * gridZ) - 2")]
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

    private bool isEvenLev = false;

    public void Start()
    {
        if (startRoom == null)
        {
            if(GameObject.FindGameObjectWithTag("Start Room") == null)
            {
                startRoom = Instantiate(startRoomPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                startRoom = GameObject.FindGameObjectWithTag("Start Room");
            }
            
        }
        if (testing)
            GenerateRooms();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            NextLevel(builtEndRoom);
        }
    }

    public GameObject firstRoom()
    {
        return startRoom;
    }

    public void GenerateRooms()
    {
        gridSpawn.SetOrigin(startRoom.transform.position);
        gridSpawn.SpawnGrid(isEvenLev);
        spawnedRooms = new GameObject[amountWanted + 2];
        spawnedRooms[0] = startRoom;
        foreach (pointProperties a in spawnedRooms[0].GetComponent<RoomAttribute>().Doorways())
        {
            if (!doorways.Contains(a))
            {
                doorways.Add(a);
            }
        }
        gridSpawn.GridPoints()[0].GetComponent<GridPoint>().hasUsed();

        spawnedRooms[1] = Instantiate(endRoomPrefab, gridSpawn.GridPoints()[gridSpawn.GridPoints().Length - 1].transform.position, Quaternion.identity);
        foreach (pointProperties a in spawnedRooms[1].GetComponent<RoomAttribute>().Doorways())
        {
            if (!doorways.Contains(a))
            {
                doorways.Add(a);
            }
        }
        builtEndRoom = spawnedRooms[1];
        gridSpawn.GridPoints()[gridSpawn.GridPoints().Length - 1].GetComponent<GridPoint>().hasUsed();

        foreach (GameObject grid in gridSpawn.GridPoints())
        {
            if(!spawnedPointsChoice.Contains(grid) && grid != null && !grid.GetComponent<GridPoint>().useCheck())
            {
                spawnedPointsChoice.Add(grid);
            }
        }
       

        spawnedPoints = new GameObject[spawnedPointsChoice.Count];
        spawnedPoints = spawnedPointsChoice.ToArray();
        int j = 2;
        //spawn rooms at random points
        while(j < spawnedRooms.Length)
        {
            UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
            int randomIndex = UnityEngine.Random.Range(0, spawnedPoints.Length);
            int randomIndex2 = UnityEngine.Random.Range(0, roomChoices.Length);
            if (!spawnedPoints[randomIndex].GetComponent<GridPoint>().useCheck())
            {
                spawnedRooms[j] = Instantiate(roomChoices[randomIndex2], spawnedPoints[randomIndex].transform.position, Quaternion.identity);
                spawnedRooms[j].transform.parent = roomParent;
                foreach (pointProperties a in spawnedRooms[j].GetComponent<RoomAttribute>().Doorways())
                {
                    if (!doorways.Contains(a))
                    {
                        doorways.Add(a);
                    }
                }
                spawnedPoints[randomIndex].GetComponent<GridPoint>().hasUsed();
                j++;
            }
        }

        pointProperties[] doorway = new pointProperties[doorways.Count];
        doorway = doorways.ToArray();
        pointProperties closestPoint = null;
        bool wasNull = false;

        //Used to connect Doorways
        foreach (pointProperties pointA in doorways)
        {
            if(!pointA.useCheck())
            {
                foreach (pointProperties pointB in doorways)
                {
                    if(closestPoint != null)
                    {
                        if(!pointB.useCheck())
                        {
                            if(pointA.parentCheck() != pointB.parentCheck())
                            {
                                if(Vector3.Distance(pointA.gameObject.transform.position, pointB.gameObject.transform.position) < Vector3.Distance(pointA.gameObject.transform.position, closestPoint.gameObject.transform.position))
                                {
                                    if (pointA.Directions()[0] && pointB.Directions()[1])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z < pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[1] && pointB.Directions()[0])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z > pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[2] && pointB.Directions()[3])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x < pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[3] && pointB.Directions()[2])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x > pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(closestPoint == null)
                        {
                            if (!pointB.useCheck())
                            {
                                if (pointA.parentCheck() != pointB.parentCheck())
                                {
                                    if (pointA.Directions()[0] && pointB.Directions()[1])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z < pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[1] && pointB.Directions()[0])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z > pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[2] && pointB.Directions()[3])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x < pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[3] && pointB.Directions()[2])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x > pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if(!wasNull && closestPoint != null)
                {
                    if(closestPoint.Directions()[0] || closestPoint.Directions()[1])
                    {
                        hallwayGeneration.CornerPoint(pointA.gameObject, closestPoint.gameObject, true);
                    }
                    else if (closestPoint.Directions()[2] || closestPoint.Directions()[3])
                    {
                        hallwayGeneration.CornerPoint(pointA.gameObject, closestPoint.gameObject, false);
                    }
                    closestPoint = null;
                    wasNull = false;
                }

            }
            
        }
        //double check connections
        foreach (pointProperties pointA in doorways)
        {
            if (!pointA.useCheck())
            {
                foreach (pointProperties pointB in doorways)
                {
                    if (closestPoint != null)
                    {
                        if (!pointB.useCheck())
                        {
                            if (pointA.parentCheck() != pointB.parentCheck())
                            {
                                if (Vector3.Distance(pointA.gameObject.transform.position, pointB.gameObject.transform.position) < Vector3.Distance(pointA.gameObject.transform.position, closestPoint.gameObject.transform.position))
                                {
                                    if (pointA.Directions()[0] && pointB.Directions()[1])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z < pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[1] && pointB.Directions()[0])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z > pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[2] && pointB.Directions()[3])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x < pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[3] && pointB.Directions()[2])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x > pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (closestPoint == null)
                        {
                            if (!pointB.useCheck())
                            {
                                if (pointA.parentCheck() != pointB.parentCheck())
                                {
                                    if (pointA.Directions()[0] && pointB.Directions()[1])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z < pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[1] && pointB.Directions()[0])
                                    {
                                        if (pointA.gameObject.transform.position.x == pointB.gameObject.transform.position.x)
                                        {
                                            if (pointA.gameObject.transform.position.z > pointB.gameObject.transform.position.z)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[2] && pointB.Directions()[3])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x < pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                    else if (pointA.Directions()[3] && pointB.Directions()[2])
                                    {
                                        if (pointA.gameObject.transform.position.z == pointB.gameObject.transform.position.z)
                                        {
                                            if (pointA.gameObject.transform.position.x > pointB.gameObject.transform.position.x)
                                            {
                                                closestPoint = pointB;
                                                wasNull = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!wasNull && closestPoint != null)
                {
                    if (closestPoint.Directions()[0] || closestPoint.Directions()[1])
                    {
                        hallwayGeneration.CornerPoint(pointA.gameObject, closestPoint.gameObject, true);
                    }
                    else if (closestPoint.Directions()[2] || closestPoint.Directions()[3])
                    {
                        hallwayGeneration.CornerPoint(pointA.gameObject, closestPoint.gameObject, false);
                    }
                    closestPoint = null;
                    wasNull = false;
                }

            }

        }
        //used to switch on and off doors
        foreach (pointProperties a in doorway)
        {
            a.doorChAct();
        }

        gridSpawn.GridDestroy();
        astarPath.Scan();

        foreach(GameObject room in spawnedRooms)
        {
            if(room.GetComponent<RoomAttribute>() != null && room != startRoom && room != builtEndRoom)
            {
                room.GetComponent<RoomAttribute>().SpawnEnemy();
            }
        }

        Resources.UnloadUnusedAssets();

    }

    public void NextLevel(GameObject endRoom)
    {
        if(endRoom != startRoom)
        {
            foreach (AstarCustom enemy in FindObjectsOfType<AstarCustom>())
            {
                enemy.forceStop();
                Destroy(enemy.gameObject);
            }

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

            isEvenLev = !isEvenLev;

            builtEndRoom = null;
            spawnedPointsChoice.Clear();
            spawnedPoints = null;
            doorways.Clear();
            Resources.UnloadUnusedAssets();
            GenerateRooms();
        }
    }

    public GameObject EndRoomCurrent()
    {
        return builtEndRoom;
    }
}
