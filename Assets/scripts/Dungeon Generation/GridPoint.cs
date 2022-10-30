using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    bool used = false;

    public bool useCheck()
    {
        return used;
    }

    public void hasUsed()
    {
        used = true;
    }
}
