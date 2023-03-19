using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vendingMachine : MonoBehaviour
{

    public GameObject spawnItem;

    public Transform spawnPoint;

    public GameObject refundMag;

    public int itemCost;

    int ammoDeposited = 0;


    public void spawnItemCheck()
    {
        if(ammoDeposited > itemCost)
        {
            ammoDeposited -= itemCost;
            Instantiate(spawnItem, spawnPoint.position, Quaternion.identity);

            Magazine magazine = refundMag.GetComponent<Magazine>();

            while(ammoDeposited > magazine.ammoValue)
            {
                ammoDeposited -= magazine.ammoValue;
                Instantiate(refundMag, spawnPoint.position, Quaternion.identity);
            }
            ammoDeposited = 0;
        }
    }

    public void depositAmmo(int ammountDeposit)
    {
        ammoDeposited += ammountDeposit;
    }

}
