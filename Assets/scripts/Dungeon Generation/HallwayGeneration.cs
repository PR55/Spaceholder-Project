using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneration : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject cornerPoint;

    public GameObject pointMarkers;
    public GameObject FloorPrefab;
    public GameObject FloorPrefabEndPoints;

    Vector2 distance;

    GameObject[] markers1;
    GameObject[] markers2;
    GameObject[] hallways;

    // Start is called before the first frame update
    void Start()
    {
        
        distance = new Vector2(endPoint.transform.position.x - startPoint.transform.position.x, endPoint.transform.position.y - startPoint.transform.position.y);
        //if(startPoint.transform.position.x > endPoint.transform.position.x)
        //{
        //    if (startPoint.transform.position.z < endPoint.transform.position.z)
        //    {
        //        cornerPoint.transform.position = new Vector3(startPoint.transform.position.x, 0, startPoint.transform.position.z);
        //    }
        //    else
        //    {
        //        cornerPoint.transform.position = new Vector3(startPoint.transform.position.x, 0, endPoint.transform.position.z);
        //    }
        //}
        //else
        //{
        //    if (startPoint.transform.position.z < endPoint.transform.position.z)
        //    {
        //        cornerPoint.transform.position = new Vector3(endPoint.transform.position.x, 0, startPoint.transform.position.z);
        //    }
        //    else
        //    {
        //        cornerPoint.transform.position = new Vector3(endPoint.transform.position.x, 0, endPoint.transform.position.z);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            
            GenerateMarkers();
        }
    }

    public void GenerateMarkers()
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
        float i = 0;
        float distance2 = Mathf.Floor(distance.x / FloorPrefab.GetComponent<FloorBehaviour>().GetSpace());
        Debug.Log(distance2.ToString());
        markers1 = new GameObject[(int)Mathf.Floor(distance2)];

        while(i < Mathf.Floor(distance2))
        {
            markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(startPoint.transform.position.x + ((i+1) * FloorPrefab.GetComponent<FloorBehaviour>().GetSpace()),0,0), new Quaternion(0,0,0,1)));

            i++;
        }
        int j = 0;
        hallways = new GameObject[markers1.Length + 2];
        if (markers1 != null)
        {
            
            hallways[j] = Instantiate(FloorPrefabEndPoints, startPoint.transform.position, FloorPrefabEndPoints.transform.rotation);
            j++;
            hallways[j] = Instantiate(FloorPrefabEndPoints, new Vector3(endPoint.transform.position.x + FloorPrefabEndPoints.GetComponent<FloorBehaviour>().GetSpace(), endPoint.transform.position.y, endPoint.transform.position.z), FloorPrefabEndPoints.transform.rotation);
            j++;
            foreach (GameObject mark in markers1)
            {
                hallways[j] = Instantiate(FloorPrefab, mark.transform.position, FloorPrefab.transform.rotation);
                j++;
            }
            for (int a = 0; a < markers1.Length; a++)
            {
                Destroy(markers1[a]);
            }
            markers1 = null;
        }

    }
}
