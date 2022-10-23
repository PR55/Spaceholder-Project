using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generaterooms : MonoBehaviour
{

    public Vector2 range = new Vector2(20, 20);

    public GameObject gridRepresent;

    public Transform gridParent;

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
    public pointProperties[] pointsDoorwaysMandatory;
    public pointProperties[] pointsDoorways;
    public HallwayGeneration hallwayGeneration;

    Vector3[] spawnPoints;
    GameObject[] spawnedPoints;

    // Start is called before the first frame update
    private void Start()
    {
        spawnMarker = Instantiate(spawnMarkerObject).transform;

        spawnMarker.position = Vector3.zero;

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
        GridCreate();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GridCreate();
        }
    }

    public void GridCreate()
    {
        if(spawnedPoints != null)
        {
            foreach (GameObject a in spawnedPoints)
            {
                if (a != null)
                {
                    Destroy(a);
                }
            }
        }
        
        int xSize = Mathf.FloorToInt(range.x/minSpacing);
        int ySize = Mathf.FloorToInt(range.y/minSpacing);
        spawnPoints = new Vector3[(xSize) * (ySize)];
        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                spawnPoints[i] = new Vector3(this.transform.position.x + ((minSpacing*(x))), 0 , this.transform.position.z + ((minSpacing * y)));
            }
        }
        spawnedPoints = new GameObject[spawnPoints.Length];
        int j = 0;
        foreach (Vector3 coord in spawnPoints)
        {
            spawnedPoints[j] = Instantiate(gridRepresent, coord, Quaternion.identity);
            spawnedPoints[j].transform.parent = gridParent;
            j += 1;
        }
    }
    public void GenerateRooms()
    {
        ClearAlls();

        if (spawnedPoints != null)
        {
            foreach (GameObject a in spawnedPoints)
            {
                if (a != null)
                {
                    Destroy(a);
                }
            }
        }
        spawnedPoints = null;

        UnityEngine.Random.seed = DateTime.UtcNow.Millisecond;
        int i = 0;
        while (i < totalAmountAllowed)
        {
            
            var RoomChoice = roomsChoice[UnityEngine.Random.Range(0, roomsChoice.Length)];
            spawnMarker.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length-1)];
            if (spawnMarker.gameObject.GetComponent<BoxChildCollider>() != null)
            {
                spawnMarker.gameObject.GetComponent<BoxChildCollider>().SetBounds(new Vector3(RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().x + minSpacing, 10, RoomChoice.GetComponent<RoomAttribute>().RoomDimensions().y + minSpacing));

                isCollide = spawnMarker.gameObject.GetComponent<BoxChildCollider>().CollideCheck();
            }
                

            if (isCollide)
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


        Resources.UnloadUnusedAssets();
        Resources.UnloadUnusedAssets();
        pointsDoorways = new pointProperties[allDoorWays.Count];
        pointsDoorways = allDoorWays.ToArray();
        pointProperties closestPoint = null;
        foreach (pointProperties a in pointsDoorways)
        {
            
            if(!a.useCheck())
            {
                foreach (pointProperties b in pointsDoorways)
                {

                    if (b != a)
                    {
                        if (b.useCheck() == false)
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
            }
            if (closestPoint != null)
                hallwayGeneration.pointCheck(a.gameObject, closestPoint.gameObject);
            closestPoint = null;
        }
        foreach (pointProperties a in pointsDoorways)
        {
            a.doorChAct();
        }
        hallways = null;
        hallways = new GameObject[hallwayGeneration.hallwayColelction().Count];
        hallways = hallwayGeneration.hallwayColelction().ToArray();
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


        hallwayGeneration.FullClear();
        Resources.UnloadUnusedAssets();
    }

    
}
