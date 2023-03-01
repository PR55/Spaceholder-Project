using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public List<GameObject> lootItems;

    bool[] hasSpawned;

    public int[] dropChances;

    private void Start()
    {
        hasSpawned = new bool[lootItems.Count];
    }

    public bool canSpawn(GameObject lootItm)
    {
        
        if(lootItems.Contains(lootItm))
        {
            int index = lootItems.IndexOf(lootItm);
            if(hasSpawned[index] == false)
            {
                UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
                int randNum = UnityEngine.Random.Range(0, 101);
                if(randNum < dropChances[index])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }
    }

    public void setSpawnState(GameObject lootItm, bool state)
    {
        if (lootItems.Contains(lootItm))
        {
            int index = lootItems.IndexOf(lootItm);
            hasSpawned[index] = state;

        }
    }
    
}
