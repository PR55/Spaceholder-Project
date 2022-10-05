using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChange : MonoBehaviour
{
    public pointProperties point;

    public GameObject wall;

    public void DoorSwitch() // used to change to doors to walls if not used as door way
    {
        wall.SetActive(!point.useCheck());
        this.gameObject.SetActive(point.useCheck());
    }
}
