using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneration : MonoBehaviour
{

    [Range(1, 20)]
    public int spaceing = 5;

    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject pointMarkers;
    public GameObject HallwayPrefab;

    Vector2 distance;

    GameObject[] markers;
    GameObject[] hallways;

    // Start is called before the first frame update
    void Start()
    {
        
        distance = new Vector2(endPoint.transform.position.x - startPoint.transform.position.x, endPoint.transform.position.y - startPoint.transform.position.y);
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
        if (markers != null)
        {
            for (int a = 0; a < markers.Length; a++)
            {
                Destroy(markers[a]);
            }
            markers = null;
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
        float distance2 = Mathf.Floor(distance.x / spaceing);
        Debug.Log(distance2.ToString());
        markers = new GameObject[(int)Mathf.Floor(distance2)];

        while(i < Mathf.Floor(distance2))
        {
            markers[(int)i] = (Instantiate(pointMarkers, new Vector3(startPoint.transform.position.x + ((i+1) * spaceing),0,0), new Quaternion(0,0,0,1)));

            i++;
        }
        int j = 0;
        hallways = new GameObject[markers.Length + 2];
        if (markers != null)
        {
            hallways[j] = Instantiate(HallwayPrefab, startPoint.transform.position, startPoint.transform.rotation);
            j++;
            hallways[j] = Instantiate(HallwayPrefab, endPoint.transform.position, endPoint.transform.rotation);
            j++;
            foreach (GameObject mark in markers)
            {
                hallways[j] = Instantiate(HallwayPrefab, mark.transform.position, mark.transform.rotation);
                j++;
            }
            markers = null;
        }

    }
}
