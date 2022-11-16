using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneration : MonoBehaviour
{
    
    [Header("Hierarchy Objects")]
    public GameObject hallwayHolder;

    GameObject collideCheck;

    [Header("Prefabs")]
    public GameObject pointMarkers;
    public GameObject FloorPrefabx;
    public GameObject FloorPrefabz;
    public GameObject collideCheckPrefab;

    private HallwayCheck hallwayCheck;

    float distance1;
    GameObject[] markers1;
    GameObject[] hallways;
    List<GameObject> allHallways = new List<GameObject>();

    private void Awake()
    {
        if(collideCheck == null)
        {
            collideCheck = Instantiate(collideCheckPrefab);
        }

        if(collideCheck.GetComponent<HallwayCheck>() != null)
        {
            hallwayCheck = collideCheck.GetComponent<HallwayCheck>();
        }
    }

    void addToList(GameObject[] hallway)
    {
        foreach(GameObject a in hallway)
        {
            if(a != null && !allHallways.Contains(a))
                allHallways.Add(a.gameObject);
        }
    }

    public List<GameObject> hallwayColelction()
    {
        return allHallways;
    }
    public void CornerPoint(GameObject point1, GameObject point2, bool isZ = false)
    {
        if(collideCheck != null)
        {
            collideCheck.transform.position = (point1.transform.position + point2.transform.position) / 2;
        }
        if(!hallwayCheck.collideCheck())
        {
            distance1 = Vector3.Distance(point1.transform.position, point2.transform.position);
            point1.GetComponent<pointProperties>().hasUsed();
            point1.GetComponent<pointProperties>().isPoint1();
            point1.GetComponent<pointProperties>().otherPint(point2);
            point2.GetComponent<pointProperties>().hasUsed();
            point2.GetComponent<pointProperties>().isPoint2();
            point2.GetComponent<pointProperties>().otherPint(point1);
            GenerateMarkers(point1, point2, isZ);
        }
    }

    
    public void FullClear()
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
        GameObject[] tempHalls = new GameObject[allHallways.Count];
        tempHalls = allHallways.ToArray();

        foreach(GameObject a in tempHalls)
        {
            Destroy(a);
        }

        allHallways = new List<GameObject>();

        Resources.UnloadUnusedAssets();
    }


    public void GenerateMarkers(GameObject pointStart, GameObject pointEnd, bool isZ = false)
    {
       
        if (isZ)
        {
            int i = 0;
            float distance3 = Mathf.Floor(distance1 / FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace());
            markers1 = new GameObject[Mathf.FloorToInt(distance3)];
            while (i < markers1.Length)
            {

                if (pointStart.transform.position.z < pointEnd.transform.position.z)
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x, 0, pointStart.transform.position.z + ((i+.5f) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())), Quaternion.identity));
                    

                }
                else
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x, 0, pointStart.transform.position.z - ((i+.5f) * FloorPrefabz.GetComponent<FloorBehaviour>().GetSpace())), Quaternion.identity));
                    
                }

                i++;
            }
            int j = 0;

            hallways = new GameObject[markers1.Length];
            if (markers1 != null)
            {
                
                    
                foreach (GameObject mark in markers1)
                {
                    if (mark.transform.position != pointEnd.transform.position)
                    {
                        hallways[j] = Instantiate(FloorPrefabz, mark.transform.position, FloorPrefabz.transform.rotation);
                        hallways[j].transform.parent = hallwayHolder.transform;
                    }

                    j++;
                }
                for (int a = 0; a < markers1.Length; a++)
                {
                    Destroy(markers1[a]);
                }
                markers1 = null;
            }
            addToList(hallways);
        }
        else
        {
            int i = 0;
            float distance3 = Mathf.Floor(distance1 / FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace());
            markers1 = new GameObject[Mathf.FloorToInt(distance3)];
            while (i < markers1.Length)
            {

                if (pointStart.transform.position.x < pointEnd.transform.position.x)
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x + (((i + .5f) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace())), 0, pointStart.transform.position.z), Quaternion.identity));
                    

                }
                else
                {
                    markers1[(int)i] = (Instantiate(pointMarkers, new Vector3(pointStart.transform.position.x - (((i + .5f) * FloorPrefabx.GetComponent<FloorBehaviour>().GetSpace())), 0, pointStart.transform.position.z), Quaternion.identity));
                    
                }

                i++;
            }
            int j = 0;

            hallways = new GameObject[markers1.Length];
            if (markers1 != null)
            {
                
                foreach (GameObject mark in markers1)
                {
                    if (mark.transform.position != pointEnd.transform.position)
                    {
                        hallways[j] = Instantiate(FloorPrefabx, mark.transform.position, FloorPrefabx.transform.rotation);
                        hallways[j].transform.parent = hallwayHolder.transform;
                    }

                    j++;
                }
                for (int a = 0; a < markers1.Length; a++)
                {
                    Destroy(markers1[a]);
                }
                markers1 = null;
            }
            addToList(hallways);
            Resources.UnloadUnusedAssets();
        }
    }
        

    
}
