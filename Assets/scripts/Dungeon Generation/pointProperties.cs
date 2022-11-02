using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class pointProperties : MonoBehaviour
{
    
    [Tooltip("Designation is relative to center of prefab world transform:\n0 - North (Z positive)\n1 - South (Z Negative)\n2 - East (X Positive)\n3 - West (X Negative)")]
    public bool[] direction = new bool[4]; // 0 - North 1 - South 2 - East 3 - West
    public GameObject doorParent;
    public GameObject overallParent;
    public int desiredLength = 5;

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

    public bool useCheck()
    {
        return used;
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
