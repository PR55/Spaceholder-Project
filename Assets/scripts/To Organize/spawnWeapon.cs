using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(weaponPrefab, this.transform.position, weaponPrefab.transform.rotation);
    }
}
