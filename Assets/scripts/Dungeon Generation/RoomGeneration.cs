using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomGeneration : MonoBehaviour
{
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

    float greatestSizeZ = 0;
    float smallestSizeZ = 0;
    float greatestSizeX = 0;
    float smallestSizeX = 0;

    [Header("Test Activation")]
    [SerializeField]
    private bool testing;

    public void Start()
    {
        foreach (GameObject a in roomChoices)//decide the close and far ranges per a point
        {
            if(greatestSizeZ == 0)
            {
                greatestSizeZ = a.GetComponent<RoomAttribute>().RoomDimensions().y;
            }
            else if (greatestSizeZ < a.GetComponent<RoomAttribute>().RoomDimensions().y)
            {
                greatestSizeZ = a.GetComponent<RoomAttribute>().RoomDimensions().y;
            }
            if (greatestSizeX == 0)
            {
                greatestSizeX = a.GetComponent<RoomAttribute>().RoomDimensions().x;
            }
            else if (greatestSizeZ < a.GetComponent<RoomAttribute>().RoomDimensions().x)
            {
                greatestSizeX = a.GetComponent<RoomAttribute>().RoomDimensions().x;
            }
            if (smallestSizeZ == 0)
            {
                smallestSizeZ = a.GetComponent<RoomAttribute>().RoomDimensions().y;
            }
            else if (smallestSizeZ > a.GetComponent<RoomAttribute>().RoomDimensions().y)
            {
                smallestSizeZ = a.GetComponent<RoomAttribute>().RoomDimensions().y;
            }
            if (smallestSizeX == 0)
            {
                smallestSizeX = a.GetComponent<RoomAttribute>().RoomDimensions().x;
            }
            else if (smallestSizeZ > a.GetComponent<RoomAttribute>().RoomDimensions().x)
            {
                smallestSizeX = a.GetComponent<RoomAttribute>().RoomDimensions().x;
            }
        }
        if (greatestSizeZ < endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().y)
        {
            greatestSizeZ = endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().y;
        }
        else if (smallestSizeZ > endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().y)
        {
            smallestSizeZ = endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().y;
        }
        if (greatestSizeX < endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().x)
        {
            greatestSizeZ = endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().x;
        }
        else if (smallestSizeX > endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().x)
        {
            smallestSizeX = endRoomPrefab.GetComponent<RoomAttribute>().RoomDimensions().x;
        }

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
        spawnedRooms = new GameObject[amountWanted + 2];
        spawnedRooms[0] = startRoom;

        int doorwayAccessIndex = 0;
        int i = 1;

        while(i < spawnedRooms.Length)
        {
            int accessIndex = 0;
            GameObject recentRoom;
            int roomIndexNotNull = 0;
            int roomIndexChoice;
            if (roomChoices.Length == 1)
            {
                roomIndexChoice = 0;
            }
            else
            {
                roomIndexChoice = Mathf.FloorToInt(Random.Range(0, roomChoices.Length - 1));
            }

            foreach(GameObject a in spawnedRooms)
            {
                if(a != null)
                {
                    roomIndexNotNull += 1;
                }
            }
            accessIndex = Mathf.FloorToInt(Random.Range(0, roomIndexNotNull));

            doorwayAccessIndex = Mathf.FloorToInt(Random.Range(0, spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways().Length - 1));

            if (!spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways()[doorwayAccessIndex].useCheck() || i == 1)
            {
                if (i == Mathf.FloorToInt(spawnedRooms.Length / 2))
                {
                    recentRoom = spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways()[doorwayAccessIndex].BuildRoom(endRoomPrefab,roomParent,hallwayParent, true);
                    if(recentRoom != null)
                    {
                        if (doorwayAccessIndex == 0)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[1].hasUsed();
                        }
                        else if (doorwayAccessIndex == 1)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[0].hasUsed();
                        }
                        else if (doorwayAccessIndex == 2)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[3].hasUsed();
                        }
                        else if (doorwayAccessIndex == 3)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[2].hasUsed();
                        }
                        spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways()[doorwayAccessIndex].hasUsed();
                        spawnedRooms[i] = recentRoom;
                        builtEndRoom = spawnedRooms[i];
                    }
                }
                else
                {
                    recentRoom = spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways()[doorwayAccessIndex].BuildRoom(roomChoices[roomIndexChoice], roomParent, hallwayParent, false);
                    if (recentRoom != null)
                    {
                        if (doorwayAccessIndex == 0)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[1].hasUsed();
                        }
                        else if (doorwayAccessIndex == 1)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[0].hasUsed();
                        }
                        else if (doorwayAccessIndex == 2)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[3].hasUsed();
                        }
                        else if (doorwayAccessIndex == 3)
                        {
                            recentRoom.GetComponent<RoomAttribute>().Doorways()[2].hasUsed();
                        }

                        spawnedRooms[accessIndex].GetComponent<RoomAttribute>().Doorways()[doorwayAccessIndex].hasUsed();
                        spawnedRooms[i] = recentRoom;
                    }
                }
                foreach (pointProperties a in FindObjectsOfType<pointProperties>())
                {
                    a.checkForRooms();
                }
                i++;
            }

        }
        foreach(pointProperties a in FindObjectsOfType<pointProperties>())
        {
            a.doorChAct();
        }
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
            Resources.UnloadUnusedAssets();
            GenerateRooms();
        }
    }

    public float XLargest()
    {
        return greatestSizeX;
    }
    public float ZLargest()
    {
        return greatestSizeZ;
    }
    public float XSmallest()
    {
        return smallestSizeX;
    }
    public float ZSmallest()
    {
        return smallestSizeZ;
    }

}
