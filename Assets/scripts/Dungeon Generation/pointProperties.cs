using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class pointProperties : MonoBehaviour
{
    
    [Tooltip("Designation is relative to center of prefabe world transform:\n0 - South (X Negative)\n1 - North (X positive)\n2 - West (Z positive)\n3 - East (Z negative)")]
    public bool[] direction = new bool[4]; // 0 - North 1 - South 2 - East 3 - West
    public GameObject doorParent;
    public GameObject overallParent;
    public GameObject floorPrefabZ;
    public GameObject floorPrefabx;
    public int desiredLength = 0;

    GameObject[] floors;
    RoomGeneration roomGeneration;

    float distanceCheckXFar;
    float distanceCheckZFar;
    float distanceCheckXClose;
    float distanceCheckZClose;

    bool used = false;
    bool point1 = false;
    bool point2 = false;
    GameObject otherPoint;

    


    private void Start()
    {
        roomGeneration = GameObject.FindObjectOfType<RoomGeneration>();
        overallParent = doorParent.transform.parent.gameObject;
        distanceCheckXFar = (floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() * desiredLength) + (roomGeneration.XLargest() * .55f);
        distanceCheckZFar = (floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() * desiredLength) + (roomGeneration.ZLargest() * .55f);
        distanceCheckXClose = (floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() * desiredLength) + (roomGeneration.XSmallest() * .55f);
        distanceCheckZClose = (floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() * desiredLength) + (roomGeneration.ZSmallest() * .55f);

    }
    public bool[] Directions()
    {
        return direction;
    }

    public void hasUsed()
    {
        used = true;
    }

    public GameObject parentCheck()
    {
        return overallParent;
    }

    public void doorChAct()
    {
        if(doorParent.GetComponent<DoorChange>())
        {
            doorParent.GetComponent<DoorChange>().DoorSwitch();
        }
    }

    public void doorReset()
    {
        if (doorParent.GetComponent<DoorChange>())
        {
            doorParent.GetComponent<DoorChange>().DoorReset();
        }
    }

    public GameObject BuildRoom(GameObject roomToBuild, Transform roomParent, Transform hallwayParent, bool isEndRoom)
    {
        if (desiredLength <= 1)
        {
            desiredLength = 2;
        }
        GameObject builtRoom = null;
        Vector3 localSpace = this.transform.position - overallParent.transform.position;
        if (direction[0])
        {
            if (desiredLength > 1)
            {
                floors = new GameObject[desiredLength];
                for (int i = 0; i < floors.Length; i++)
                {
                    if (i == 0)
                    {
                        floors[i] = Instantiate(floorPrefabx, new Vector3((this.transform.position.x - ((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() / 2) * (i + 1))), this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }
                    else
                    {
                        floors[i] = Instantiate(floorPrefabx, new Vector3((floors[i - 1].transform.position.x - ((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace()))), this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }

                }
                builtRoom = floors[floors.Length - 1].GetComponent<FloorBehaviour>().BuildRoom(direction, overallParent.transform.parent.transform.position, roomToBuild, roomParent, isEndRoom);
            }
        }
        else if (direction[1])
        {
            if (desiredLength > 1)
            {
                floors = new GameObject[desiredLength];
                for (int i = 0; i < floors.Length; i++)
                {
                    if (i == 0)
                    {
                        floors[i] = Instantiate(floorPrefabx, new Vector3((this.transform.position.x + ((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() / 2) * (i + 1))), this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }
                    else
                    {
                        floors[i] = Instantiate(floorPrefabx, new Vector3((floors[i - 1].transform.position.x + ((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace()))), this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }

                }
                builtRoom = floors[floors.Length - 1].GetComponent<FloorBehaviour>().BuildRoom(direction, overallParent.transform.parent.transform.position, roomToBuild, roomParent, isEndRoom);
            }
        }
        else if (direction[2])
        {
            if (desiredLength > 1)
            {
                floors = new GameObject[desiredLength];
                for (int i = 0; i < floors.Length; i++)
                {
                    if (i == 0)
                    {
                        floors[i] = Instantiate(floorPrefabZ, new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + ((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() / 2) * (i + 1)))), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }
                    else
                    {
                        floors[i] = Instantiate(floorPrefabZ, new Vector3(this.transform.position.x, this.transform.position.y, (floors[i - 1].transform.position.z + ((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace())))), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }

                }
                builtRoom = floors[floors.Length - 1].GetComponent<FloorBehaviour>().BuildRoom(direction, overallParent.transform.parent.transform.position, roomToBuild, roomParent, isEndRoom);
            }
        }
        else if (direction[3])
        {
            if (desiredLength > 1)
            {
                floors = new GameObject[desiredLength];
                for (int i = 0; i < floors.Length; i++)
                {
                    if (i == 0)
                    {
                        floors[i] = Instantiate(floorPrefabZ, new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - ((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() / 2) * (i + 1)))), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }
                    else
                    {
                        floors[i] = Instantiate(floorPrefabZ, new Vector3(this.transform.position.x, this.transform.position.y, (floors[i - 1].transform.position.z - ((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace())))), Quaternion.identity);
                        floors[i].transform.parent = hallwayParent;
                    }

                }
                builtRoom = floors[floors.Length - 1].GetComponent<FloorBehaviour>().BuildRoom(direction, overallParent.transform.parent.transform.position, roomToBuild, roomParent, isEndRoom);
            }

        }
        return builtRoom;
    }

    public bool useCheck()
    {
        return used;
    }

    public void checkForRooms()
    {
        pointProperties[] pointProperties = new pointProperties[GameObject.FindObjectsOfType<pointProperties>().Length - 1];
        int i = 0;
        foreach(pointProperties a in GameObject.FindObjectsOfType<pointProperties>())
        {
            if(a.gameObject != this.gameObject && a.parentCheck() != parentCheck())
            {
                if(!a.useCheck())
                {
                    pointProperties[i] = a;
                    i++;
                }
            }
        }

        foreach(pointProperties point in pointProperties)
        {
            if (point != null)
            {
                if (direction[0])
                {
                    if (point.Directions()[1])
                    {
                        if (distanceCheckXClose - 5 <= Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) && Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) <= distanceCheckXFar + 5)
                        {
                            point.hasUsed();
                        }
                    }
                }
                else if (direction[1])
                {
                    if (point.Directions()[0])
                    {
                        if (distanceCheckXClose - 5 <= Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) && Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) <= distanceCheckXFar + 5)
                        {
                            point.hasUsed();
                        }
                    }
                }
                else if (direction[2])
                {
                    if (point.Directions()[3])
                    {
                        if (distanceCheckZClose - 5 <= Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) && Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) <= distanceCheckZFar + 5)
                        {
                            point.hasUsed();
                        }
                    }
                }
                else if (direction[3])
                {
                    if (point.Directions()[2])
                    {
                        if (distanceCheckZClose - 5 <= Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) && Vector3.Distance(this.gameObject.transform.position, point.gameObject.transform.position) <= distanceCheckZFar + 5)
                        {
                            point.hasUsed();
                        }
                    }
                }
            }
            
        }

    }

    public Vector3[] SizeBoxDistance(GameObject roomToBuild)
    {
        Vector3[] dimensions = new Vector3[2];
        dimensions[0] = Vector3.zero; // position
        dimensions[1] = Vector3.zero; // size
        if (direction[0])
        {
            dimensions[0] = new Vector3(this.transform.position.x - (((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() / 2) * (desiredLength)) + (roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x * .55f)), this.transform.position.y, this.transform.position.z);
            dimensions[1] = new Vector3(roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x/2, 10, roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y / 2);
        }
        else if (direction[1])
        {
            dimensions[0] = new Vector3(this.transform.position.x + (((floorPrefabx.GetComponent<FloorBehaviour>().GetSpace() / 2) * (desiredLength)) + (roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x * .55f)), this.transform.position.y, this.transform.position.z);
            dimensions[1] = new Vector3(roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x / 2, 10, roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y / 2);
        }
        else if (direction[2])
        {
            dimensions[0] = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + (((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() / 2) * (desiredLength)) + (roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f))));
            dimensions[1] = new Vector3(roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x / 2, 10, roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y / 2);
        }
        else if (direction[3])
        {
            dimensions[0] = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - (((floorPrefabZ.GetComponent<FloorBehaviour>().GetSpace() / 2) * (desiredLength)) + (roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f))));
            dimensions[1] = new Vector3(roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().x / 2, 10, roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y / 2);
        }
        return dimensions;
    }

    public void isPoint1()
    {
        point1 = true;
    }
    public void isPoint2()
    {
        point2 = true;
    }
    public bool Point1()
    {
        return point1;
    }
    public bool Point2()
    {
        return point2;
    }
    public void otherPint(GameObject oPoint)
    {
        otherPoint = oPoint;
    }
    public void pointReset()
    {
        otherPoint = null;
        point1 = false;
        point2 = false;
        used = false;
    }
    private void OnDrawGizmos()
    {
        if(point1)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(this.gameObject.transform.position, otherPoint.transform.position);
        }
        else if (point2)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(this.gameObject.transform.position, otherPoint.transform.position);
        }
    }

}
