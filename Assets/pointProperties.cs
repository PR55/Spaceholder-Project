using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointProperties : MonoBehaviour
{
    public bool[] direction = new bool[4]; // 0 - North 1 - South 2 - East 3 - West

    bool used = false;


    public bool[] Directions()
    {
        return direction;
    }

    public void hasUsed()
    {
        used = true;
    }

    public bool useCheck()
    {
        return used;
    }

}
