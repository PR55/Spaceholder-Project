using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneration : MonoBehaviour
{
    

    public GameObject hallwayHolder;

    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject cornerPoint;

    public GameObject pointMarkers;
    public GameObject FloorPrefabx;
    public GameObject FloorPrefabz;
    public GameObject cornerPrefab;
    public GameObject cornerPointPrefab;

    float distance1;
    float distance2;

    public float minDoorwaySpacing = 10f;

    GameObject[] markers1;
    GameObject[] markers2;
    GameObject[] hallways;

    public GameObject[] hallwayColelction()
    {
        int i = 0;
        hallways = null;
        hallways = new GameObject[GameObject.FindObjectsOfType<FloorBehaviour>().Length];
        foreach (FloorBehaviour floor in GameObject.FindObjectsOfType<FloorBehaviour>())
        {
            hallways[i] = floor.gameObject;
            i++;
        }
        return hallways;
    }
    public void pointCheck(GameObject point1, GameObject point2)
    {
        if(point1.GetComponent<pointProperties>() != null && point2.GetComponent<pointProperties>() != null)
        {
            //RELATIVE TO PARENT PREFAB:
            //(EX: FOR EAST (3) CONNECTING TO SOUTH (0) CHECK IF EAST IS BELOW THE SOUTH POINT AND THE SOUTH POINT IS TO THE RIGHT OF THE EAST POINT)
            //0 - South (X Negative)
            //1 - North (X positive)
            //2 - West (Z positive)
            //3 - East (Z negative)

            if (point1.GetComponent<pointProperties>().Directions()[3])// if east
            {
                if(point2.GetComponent<pointProperties>().Directions()[1]) // if north
                {
                    if(point1.transform.position.z - minDoorwaySpacing > point2.transform.position.z) // checking if to the right of point 1
                    {
                        if(point1.transform.position.x - minDoorwaySpacing > point2.transform.position.x) // checking if below point 1
                        {
                            CornerPoint(point1, point2);
                        }
                    }
                }
                else if (point2.GetComponent<pointProperties>().Directions()[0])// if south
                {
                    if (point1.transform.position.z - minDoorwaySpacing > point2.transform.position.z) // checking if to the right of point 1
                    {
                        if (point1.transform.position.x + minDoorwaySpacing < point2.transform.position.x)// checking if above
                        {
                            CornerPoint(point1, point2);
                        }
                    }
                }
            }
            else if(point1.GetComponent<pointProperties>().Directions()[2]) // if west
            {
                if (point2.GetComponent<pointProperties>().Directions()[1]) // if north
                {
                    if (point1.transform.position.z + minDoorwaySpacing < point2.transform.position.z) // check if to left of point 1
                    {
                        if (point1.transform.position.x - minDoorwaySpacing > point2.transform.position.x)// check if below point 1
                        {
                            CornerPoint(point1, point2);
                        }
                    }
                }
                else if (point2.GetComponent<pointProperties>().Directions()[0]) // if south
                {
                    if (point1.transform.position.z + minDoorwaySpacing < point2.transform.position.z) // check if point 1 is to right of desired connection
                    {
                        //Debug.Log("Point 1 left");
                        if (point1.transform.position.x + minDoorwaySpacing < point2.transform.position.x) // check if desired connection above point 1
                        {
                            //Debug.Log("Point 2 up");
                            CornerPoint(point1, point2);
                            
                        }
                    }
                }
            }
            else if (point1.GetComponent<pointProperties>().Directions()[1]) //if north
            {
                if (point2.GetComponent<pointProperties>().Directions()[2])// if west
                {
                    if (point1.transform.position.z - minDoorwaySpacing > point2.transform.position.z) // north doorway left of west
                    {
                        if (point1.transform.position.x + minDoorwaySpacing < point2.transform.position.x)  // north below west
                        {
                            CornerPoint(point2, point1);
                        }
                    }
                }
                else if (point2.GetComponent<pointProperties>().Directions()[3])// if east
                {
                    if (point1.transform.position.z + minDoorwaySpacing < point2.transform.position.z) // north doorway left of east
                    {
                        if (point1.transform.position.x + minDoorwaySpacing < point2.transform.position.x)// north below east
                        {
                            CornerPoint(point2, point1);
                        }
                    }
                }
            }
            else if (point1.GetComponent<pointProperties>().Directions()[0])// if south
            {
                if (point2.GetComponent<pointProperties>().Directions()[2])// if west
                {
                    if (point1.transform.position.z - minDoorwaySpacing > point2.transform.position.z)// south left of west
                    {
                        if (point1.transform.position.x - minDoorwaySpacing > point2.transform.position.x)// south above west
                        {
                            CornerPoint(point2, point1);
                        }
                    }
                }
                else if (point2.GetComponent<pointProperties>().Directions()[3])// if east
                {
                    if (point1.transform.position.z + minDoorwaySpacing < point2.transform.position.z) // south right of east
                    {
                        if (point1.transform.position.x - minDoorwaySpacing > point2.transform.position.x) // south above east
                        {
                            CornerPoint(point2, point1);
                        }
                    }
                }

            }
        }
        else
        {

            Debug.LogError("Both points need point properties script!!!!");
        }

    }


    public void CornerPoint(GameObject point1, GameObject point2)
    {
        if(cornerPoint != null)
        Destroy(cornerPoint);

        if(point1.transform.position.z == point2.transform.position.z)
        {
            distance1 = Vector3.Distance(point1.transform.position, point2.transform.position);
            if (!Physics.CheckBox(new Vector3((point2.transform.position.x - point1.transform.position.x) / 2, .1f, point1.transform.position.z), new Vector3((distance1) / 2, 10, 2.5f)))
            {
                point1.GetComponent<pointProperties>().hasUsed();
                point1.GetComponent<pointProperties>().isPoint1();
                point1.GetComponent<pointProperties>().otherPint(point2);
                point2.GetComponent<pointProperties>().hasUsed();
                point2.GetComponent<pointProperties>().isPoint2();
                point2.GetComponent<pointProperties>().otherPint(point1);
                GenerateMarkers(point1, point2, null, false);

            }
        }
        else if(point1.transform.position.x == point2.transform.position.x)
        {
            distance1 = Vector3.Distance(point1.transform.position, point2.transform.position);
            if (!Physics.CheckBox(new Vector3(point1.transform.position.x, 0, (point2.transform.position.z - point1.transform.position.z)/2), new Vector3(2.5f, 10, (distance1)/2)))
            {
                point1.GetComponent<pointProperties>().hasUsed();
                point1.GetComponent<pointProperties>().isPoint1();
                point1.GetComponent<pointProperties>().otherPint(point2);
                point2.GetComponent<pointProperties>().hasUsed();
                point2.GetComponent<pointProperties>().isPoint2();
                point2.GetComponent<pointProperties>().otherPint(point1);
                GenerateMarkers(point1, point2, null, true);
                
            }
                
        }
        else
        {
            cornerPoint = Instantiate(cornerPointPrefab, new Vector3(point1.transform.position.x, 0, point2.transform.position.z), Quaternion.identity);

            distance1 = Vector3.Distance(point1.transform.position, cornerPoint.transform.position);
            distance2 = Vector3.Distance(point2.transform.position, cornerPoint.transform.position);
            if (!Physics.CheckBox(new Vector3((cornerPoint.transform.position.x - point1.transform.position.x)/2, 0, cornerPoint.transform.position.z), new Vector3((distance1)/2, 10, (distance2) / 2)) && !Physics.CheckBox(new Vector3(cornerPoint.transform.position.x, 0, (cornerPoint.transform.position.z - point2.transform.position.z)/2), new Vector3((distance2) / 2, 10, (distance1)/2)))
            {
                point1.GetComponent<pointProperties>().hasUsed();
                point1.GetComponent<pointProperties>().isPoint1();
                point1.GetComponent<pointProperties>().otherPint(point2);
                point2.GetComponent<pointProperties>().hasUsed();
                point2.GetComponent<pointProperties>().isPoint2();
                point2.GetComponent<pointProperties>().otherPint(point1);
                GenerateMarkers(point1, point2, cornerPoint);
            }
                
        }
        
    }

    
    public void ClearAll()
    {
        if (markers1 != null)
        {
            for (int a = 0; a < markers1.Length; a++)
            {
                Destroy(markers1[a]);
            }
            markers1 = null;
        }
        if (markers2 != null)
        {
            for (int a = 0; a < markers2.Length; a++)
            {
                Destroy(markers2[a]);
            }
            markers2 = null;
        }
        if (hallways != null)
        {
            for (int a = 0; a < hallways.Length; a++)
            {
                Destroy(hallways[a]);
            }
            hallways = null;
        }
        Resources.UnloadUnusedAssets();
    }


    public void GenerateMarkers(GameObject pointStart, GameObject pointEnd , GameObject pointMid = null, bool isZ = false)
    {

        if(pointMid != null)
        {
            float i = 0;
            float distance3 = Mathf.Floor((distance1 / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace())-((cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().x/2)/ FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()));
            float distance4 = Mathf.Floor((distance2 / FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace()) - ((cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().y/2) / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()));
            markers1 = null;
            
            Debug.Log("Markers 1 size: " + Mathf.CeilToInt(distance3).ToString());
            markers1 = null;
            if (Mathf.CeilToInt(distance3) < 0)
            {
                Debug.Log("Markers 1 size: " + Mathf.CeilToInt(distance3).ToString());
                markers1 = new GameObject[Mathf.CeilToInt(distance3)*-1];
            }
            else
            {
                Debug.Log("Markers 1 size: " + Mathf.CeilToInt(distance3).ToString());
                markers1 = new GameObject[Mathf.CeilToInt(distance3)];
            }

            markers2 = null;
            if(Mathf.CeilToInt(distance4) < 0)
            {
                Debug.Log("Markers 2 size: " + Mathf.CeilToInt(distance4).ToString());
                markers2 = new GameObject[Mathf.CeilToInt(distance4)*-1];
            }
            else
            {
                Debug.Log("Markers 2 size: " + Mathf.CeilToInt(distance4).ToString());
                markers2 = new GameObject[Mathf.CeilToInt(distance4)];
            }
            
            while (i < markers1.Length)
            {

                if (pointStart.transform.position.z < pointMid.transform.position.z)
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x , 0, ((pointEnd.transform.position.z - (((i + 1)) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace()) - (cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().y / 2)))), new Quaternion(0, 0, 0, 1)));
                }
                else
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x , 0, ((pointEnd.transform.position.z + (((i + 1)) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace()) + (cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().y / 2)))), new Quaternion(0, 0, 0, 1)));
                }

                i++;
            }
            i = 0;
            while (i <markers2.Length)
            {
                if (pointEnd.transform.position.x > pointMid.transform.position.x)
                {
                    markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x + ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()) + (cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().x / 2), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
                }
                else
                {
                    markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x - ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()) - (cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().x / 2), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
                }


                i++;
            }
            int j = 0;
            hallways = new GameObject[markers1.Length + markers2.Length + 1];
            if (markers1 != null && markers2 != null)
            {
                //hallways[j] = Instantiate(FloorPrefabz, pointStart.transform.position, FloorPrefabz.transform.rotation);
                //hallways[j].transform.parent = hallwayHolder.transform;
                //j++;
                foreach (GameObject mark in markers2)
                {
                    if(mark.transform.position != pointMid.transform.position)
                    {
                        hallways[j] = Instantiate(FloorPrefabx, mark.transform.position, FloorPrefabx.transform.rotation);
                        hallways[j].transform.parent = hallwayHolder.transform;
                    }
                    
                    j++;
                }
                hallways[j] = Instantiate(cornerPrefab, pointMid.transform.position, cornerPrefab.transform.rotation);
                hallways[j].transform.parent = hallwayHolder.transform;
                j++;
                foreach (GameObject mark in markers1)
                {
                    if (mark.transform.position != pointMid.transform.position)
                    {
                        hallways[j] = Instantiate(FloorPrefabz, mark.transform.position, FloorPrefabz.transform.rotation);
                        hallways[j].transform.parent = hallwayHolder.transform;
                    }
                    j++;
                }
                //hallways[j] = Instantiate(FloorPrefabx, pointEnd.transform.position, FloorPrefabx.transform.rotation);
                //hallways[j].transform.parent = hallwayHolder.transform;
                //j++;
                for (int a = 0; a < markers1.Length; a++)
                {
                    Destroy(markers1[a]);
                }
                for (int b = 0; b < markers2.Length; b++)
                {
                    Destroy(markers2[b]);
                }
                markers1 = null;
                markers2 = null;
            }
            foreach(GameObject a in hallways)
            {
                if(a.GetComponent<DoorChangeCorner>() != null)
                {
                    if (pointStart.GetComponent<pointProperties>().direction[0])
                    {
                        if (pointEnd.GetComponent<pointProperties>().direction[2])
                        {
                            bool[] changes = { true, false, true, false };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                        else if (pointEnd.GetComponent<pointProperties>().direction[3])
                        {
                            bool[] changes = { true, false, false, true };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                    }
                    else if (pointStart.GetComponent<pointProperties>().direction[1])
                    {
                        if (pointEnd.GetComponent<pointProperties>().direction[2])
                        {
                            bool[] changes = { false, true, true, false };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                        else if (pointEnd.GetComponent<pointProperties>().direction[3])
                        {
                            bool[] changes = { false, true, false, true };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                    }
                    else if (pointStart.GetComponent<pointProperties>().direction[2])
                    {
                        if (pointEnd.GetComponent<pointProperties>().direction[0])
                        {
                            bool[] changes = { true, false, true, false };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                        else if (pointEnd.GetComponent<pointProperties>().direction[1])
                        {
                            bool[] changes = { false, true, true, false };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                    }
                    else if (pointStart.GetComponent<pointProperties>().direction[3])
                    {
                        if (pointEnd.GetComponent<pointProperties>().direction[0])
                        {
                            bool[] changes = { true, false, false, true };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                        else if (pointEnd.GetComponent<pointProperties>().direction[1])
                        {
                            bool[] changes = { false, true, false, true };
                            a.gameObject.GetComponent<DoorChangeCorner>().doorChange(changes);
                        }
                    }
                }
            }

        }
        else
        {
            float i = 0;

            

            if (isZ)
            {
                float distance3 = Mathf.Floor(distance1 / FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace());
                markers1 = new GameObject[Mathf.CeilToInt(distance3)];
                while (i < markers1.Length)
                {

                    if (pointStart.transform.position.z < pointEnd.transform.position.z)
                    {
                        markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x, 0, pointStart.transform.position.z + ((i + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())), new Quaternion(0, 0, 0, 1)));
                    }
                    else
                    {
                        markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x, 0, pointStart.transform.position.z - ((i + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())), new Quaternion(0, 0, 0, 1)));
                    }

                    i++;
                }
                int j = 0;

                hallways = new GameObject[markers1.Length + 2];
                if (markers1 != null)
                {
                    hallways[j] = Instantiate(FloorPrefabz, pointStart.transform.position, FloorPrefabz.transform.rotation);
                    hallways[j].transform.parent = hallwayHolder.transform;
                    j++;
                    foreach (GameObject mark in markers1)
                    {
                        if (mark.transform.position != pointEnd.transform.position)
                        {
                            hallways[j] = Instantiate(FloorPrefabz, mark.transform.position, FloorPrefabz.transform.rotation);
                            hallways[j].transform.parent = hallwayHolder.transform;
                        }

                        j++;
                    }
                    hallways[j] = Instantiate(FloorPrefabz, pointEnd.transform.position, FloorPrefabz.transform.rotation);
                    hallways[j].transform.parent = hallwayHolder.transform;
                    j++;
                    for (int a = 0; a < markers1.Length; a++)
                    {
                        Destroy(markers1[a]);
                    }
                    markers1 = null;
                }
            }
            else
            {
                float distance3 = Mathf.Floor(distance1 / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace());
                markers1 = new GameObject[Mathf.CeilToInt(distance3)];
                while (i < markers1.Length)
                {

                    if (pointStart.transform.position.x < pointEnd.transform.position.x)
                    {
                        markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x + ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, pointStart.transform.position.z), new Quaternion(0, 0, 0, 1)));
                    }
                    else
                    {
                        markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x - ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, pointStart.transform.position.z), new Quaternion(0, 0, 0, 1)));
                    }

                    i++;
                }
                int j = 0;

                hallways = new GameObject[markers1.Length + 2];
                if (markers1 != null)
                {
                    hallways[j] = Instantiate(FloorPrefabx, pointStart.transform.position, FloorPrefabx.transform.rotation);
                    hallways[j].transform.parent = hallwayHolder.transform;
                    j++;
                    foreach (GameObject mark in markers1)
                    {
                        if (mark.transform.position != pointEnd.transform.position)
                        {
                            hallways[j] = Instantiate(FloorPrefabx, mark.transform.position, FloorPrefabx.transform.rotation);
                            hallways[j].transform.parent = hallwayHolder.transform;
                        }

                        j++;
                    }
                    hallways[j] = Instantiate(FloorPrefabx, pointEnd.transform.position, FloorPrefabx.transform.rotation);
                    hallways[j].transform.parent = hallwayHolder.transform;
                    j++;
                    for (int a = 0; a < markers1.Length; a++)
                    {
                        Destroy(markers1[a]);
                    }
                    markers1 = null;
                }
            }


            Debug.Log("Straight hallway");
        }
        

    }
}
