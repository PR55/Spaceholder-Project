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

    GameObject[] markers1;
    GameObject[] markers2;
    GameObject[] hallways;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(startPoint.GetComponent<pointProperties>().direction[0] || startPoint.GetComponent<pointProperties>().direction[1])
            {
                CornerPoint(startPoint, endPoint);
            }
            else if (startPoint.GetComponent<pointProperties>().direction[3] || startPoint.GetComponent<pointProperties>().direction[4])
            {
                CornerPoint(endPoint, startPoint);
            }

        }
    }
    public void CornerPoint(GameObject point1, GameObject point2)
    {
        ClearAll();
        
        if(cornerPoint != null)
        Destroy(cornerPoint);

        if(point1.transform.position.x == point2.transform.position.x || point1.transform.position.z == point2.transform.position.z)
        {
            GenerateMarkers(point1, point2);
        }
        else
        {
            cornerPoint = Instantiate(cornerPointPrefab, new Vector3(point2.transform.position.x, 0, point1.transform.position.z), Quaternion.identity);

            distance1 = Vector3.Distance(point1.transform.position, cornerPoint.transform.position);
            distance2 = Vector3.Distance(point2.transform.position, cornerPoint.transform.position);
            GenerateMarkers(point1, point2, cornerPoint);
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
        if (hallways != null)
        {
            for (int a = 0; a < hallways.Length; a++)
            {
                Destroy(hallways[a]);
            }
            hallways = null;
        }
    }


    public void GenerateMarkers(GameObject pointStart, GameObject pointEnd , GameObject pointMid = null)
    {

        if(pointMid != null)
        {
            float i = 0;
            float distance3 = Mathf.Floor((distance1- (cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().y/2)) / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace());
            float distance4 = Mathf.Floor((distance2-(cornerPrefab.GetComponent<RoomAttribute>().RoomDimensions().x/2)) / FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace());
            markers1 = new GameObject[Mathf.CeilToInt(distance3)];
            markers2 = new GameObject[Mathf.CeilToInt(distance4)];
            while (i < markers1.Length)
            {

                if (pointStart.transform.position.x < pointMid.transform.position.x)
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x + ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
                }
                else
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x - ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
                }

                i++;
            }
            i = 0;
            while (i <markers2.Length)
            {
                if (pointEnd.transform.position.z > pointMid.transform.position.z)
                {
                    markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x, 0, ((pointEnd.transform.position.z - (((i+1) + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())))), new Quaternion(0, 0, 0, 1)));
                }
                else
                {
                    markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x, 0, ((pointEnd.transform.position.z + ((i + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())))), new Quaternion(0, 0, 0, 1)));
                }


                i++;
            }
            int j = 0;
            hallways = new GameObject[markers1.Length + markers2.Length + 1];
            if (markers1 != null && markers2 != null)
            {

                foreach (GameObject mark in markers1)
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
                foreach (GameObject mark in markers2)
                {
                    if (mark.transform.position != pointMid.transform.position)
                    {
                        hallways[j] = Instantiate(FloorPrefabx, mark.transform.position, FloorPrefabz.transform.rotation);
                        hallways[j].transform.parent = hallwayHolder.transform;
                    }
                    j++;
                }
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

        }
        

    }
}
