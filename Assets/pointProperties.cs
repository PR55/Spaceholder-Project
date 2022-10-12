using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class pointProperties : MonoBehaviour
{
    
    [Tooltip("0 - South\n1 - North\n2 - West\n3 - East")]
    public bool[] direction = new bool[4]; // 0 - North 1 - South 2 - East 3 - West
    public GameObject doorParent;
    public GameObject overallParent;

    bool used = false;
    bool point1 = false;
    bool point2 = false;
    GameObject otherPoint;
    private void Start()
    {
        overallParent = doorParent.transform.parent.gameObject;
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
