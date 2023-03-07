using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int ammoValue = 10;

    public int shotCount = 20;


    public bool shotFired()
    {
        if(shotCount > 0)
        {
            shotCount -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
