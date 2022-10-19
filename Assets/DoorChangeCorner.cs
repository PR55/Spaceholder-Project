using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChangeCorner : MonoBehaviour
{
    [Tooltip("Keep in same sequential order for doors:\n0 - South (X Negative)\n1 - North (X positive)\n2 - West (Z positive)\n3 - East (Z negative)")]
    public GameObject[] doors = new GameObject[4];
    [Tooltip("same order for walls:\n0 - South (X Negative)\n1 - North (X positive)\n2 - West (Z positive)\n3 - East (Z negative)")]
    public GameObject[] walls = new GameObject[4];


    public void doorChange(bool[] changes)
    {
        int i = 0;
        while(i < doors.Length)
        {
            doors[i].SetActive(!changes[i]);
            walls[i].SetActive(changes[i]);
            i++;
        }
        
    }
    
}
