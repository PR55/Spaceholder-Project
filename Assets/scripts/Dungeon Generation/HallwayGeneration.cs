using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneration : MonoBehaviour
{
    [Range(0f , 1f)]
    public float decreaseModifier = 1f;

    public GameObject hallwayHolder;

    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject cornerPoint;

    public GameObject pointMarkers;
    public GameObject FloorPrefabx;
    public GameObject FloorPrefabz;
    public GameObject FloorPrefabEndPoints;
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

            CornerPoint(startPoint, endPoint);
        }
    }
    public void CornerPoint(GameObject point1, GameObject point2)
    {
        ClearAll();
        Destroy(cornerPoint);
        cornerPoint = Instantiate(cornerPointPrefab, new Vector3(point2.transform.position.x, 0, point1.transform.position.z), Quaternion.identity);
        distance1 = Vector3.Distance(startPoint.transform.position, cornerPoint.transform.position) - (Vector3.Distance(startPoint.transform.position, cornerPoint.transform.position) * decreaseModifier);
        distance2 = Vector3.Distance(endPoint.transform.position, cornerPoint.transform.position) - (Vector3.Distance(endPoint.transform.position, cornerPoint.transform.position) * decreaseModifier);
        GenerateMarkers(point1, point2);
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


    public void GenerateMarkers(GameObject point1, GameObject point2)
    {
        float i = 0;
        float distance3 = Mathf.Floor(distance1 / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace());
        float distance4 = Mathf.Floor(distance2 / FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace());
        markers1 = new GameObject[(int)Mathf.Floor(distance3)];
        markers2 = new GameObject[(int)Mathf.Floor(distance4)];
        while (i < Mathf.Floor(distance3))
        {
            
            if (point1.transform.position.x < cornerPoint.transform.position.x)
            {
                markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(point1.transform.position.x + ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
            }
            else
            {
                markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(point1.transform.position.x - ((i + 1) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace()), 0, cornerPoint.transform.position.z), new Quaternion(0, 0, 0, 1)));
            }

                i++;
        }
        i = 0;
        while (i < Mathf.Floor(distance4))
        {
            if(point2.transform.position.z > cornerPoint.transform.position.z)
            {
                markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x, 0, ((point2.transform.position.z - ((i + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())))), new Quaternion(0, 0, 0, 1)));
            }
            else
            {
                markers2[(int)i] = (Instantiate(pointMarkers, new Vector3(cornerPoint.transform.position.x, 0, ((point2.transform.position.z + ((i + 1) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())))), new Quaternion(0, 0, 0, 1)));
            }
            

            i++;
        }
        int j = 0;
        hallways = new GameObject[markers1.Length + markers2.Length + 1];
        if (markers1 != null && markers2 != null)
        {
            
            foreach (GameObject mark in markers1)
            {
                hallways[j] = Instantiate(FloorPrefabx, mark.transform.position, FloorPrefabx.transform.rotation);
                hallways[j].transform.parent = hallwayHolder.transform;
                j++;
            }
            hallways[j] = Instantiate(FloorPrefabx, cornerPoint.transform.position, FloorPrefabx.transform.rotation);
            hallways[j].transform.parent = hallwayHolder.transform;
            j++;
            foreach (GameObject mark in markers2)
            {
                hallways[j] = Instantiate(FloorPrefabz, mark.transform.position, FloorPrefabz.transform.rotation);
                hallways[j].transform.parent = hallwayHolder.transform;
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
