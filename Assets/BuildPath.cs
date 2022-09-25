using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPath : MonoBehaviour
{
    [Range(1f , 10f)]
    public float maxPointDistance = 2;

    public GameObject cornerPointObject;

    

    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject[] corners;

    float distance;

    Vector4 Randomrange;

    Transform lastpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GeneratePath(startPoint, endPoint);
        }
    }

    public void GeneratePath(GameObject start, GameObject End)
    {
        if(corners != null)
        {
            for (int a = 0; a < corners.Length; a++)
            {
                Destroy(corners[a]);
            }
            corners = null;
        }
        distance = Vector3.Distance(start.transform.position, End.transform.position);

        lastpoint = start.transform;

        Randomrange = new Vector4(lastpoint.position.x + maxPointDistance, lastpoint.position.x - maxPointDistance, lastpoint.position.z + maxPointDistance, lastpoint.position.z - maxPointDistance);

        corners = new GameObject[Mathf.RoundToInt(distance / maxPointDistance) + 1];

        int i = 0;

        int randomNum = 0;

        while(i < corners.Length)
        {
            UnityEngine.Random.seed = DateTime.UtcNow.Millisecond;

            randomNum = UnityEngine.Random.Range(0, 5);

            if(randomNum == 0)
            {
                corners[i] = Instantiate(cornerPointObject, new Vector3(Randomrange.x, 0, lastpoint.position.z), Quaternion.identity);
            }
            else if(randomNum == 1)
            {
                corners[i] = Instantiate(cornerPointObject, new Vector3(Randomrange.y, 0, lastpoint.position.z), Quaternion.identity);
            }
            else if (randomNum == 2)
            {
                corners[i] = Instantiate(cornerPointObject, new Vector3(lastpoint.position.x, 0, Randomrange.z), Quaternion.identity);
            }
            else if (randomNum == 3)
            {
                corners[i] = Instantiate(cornerPointObject, new Vector3(lastpoint.position.x, 0, Randomrange.w), Quaternion.identity);
            }
            if (corners[i] != null)
            {
                if (distance < Vector3.Distance(corners[i].transform.position, End.transform.position))
                {
                    Destroy(corners[i]);
                }
                else
                {
                    lastpoint = corners[i].transform;

                    distance = Vector3.Distance(lastpoint.transform.position, End.transform.position);

                    Randomrange = new Vector4(lastpoint.position.x + maxPointDistance, lastpoint.position.x - maxPointDistance, lastpoint.position.z + maxPointDistance, lastpoint.position.z - maxPointDistance);

                    lastpoint.parent = this.gameObject.transform;

                    i++;
                }

            }
                

            
        }


    }

}
