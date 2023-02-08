using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawn : MonoBehaviour
{
    public GameObject gridPoint;

    public int gridX;
    public int gridZ;
    public float spacingOffsetX = 115;
    public float spacingOffsetZ = 115;

    Vector3 gridOrigin = Vector3.zero;

    GameObject[] gridPoints;

    public void SpawnGrid(bool State)
    {
        gridPoints = null;
        GridDestroy();
        gridPoints = new GameObject[gridX * gridZ];
        int i = 0;


        if(!State)
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 spawnPosition = new Vector3(x * spacingOffsetX, 0, z * spacingOffsetZ) + gridOrigin;
                    gridPoints[i] = Instantiate(gridPoint, spawnPosition, Quaternion.identity);
                    i++;
                }
            }
        }
        else
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 spawnPosition = gridOrigin - new Vector3(x * spacingOffsetX, 0, z * spacingOffsetZ);
                    gridPoints[i] = Instantiate(gridPoint, spawnPosition, Quaternion.identity);
                    i++;
                }
            }
        }
        
    }

    public GameObject[] GridPoints()
    {
        return gridPoints;
    }

    public void GridDestroy()
    {
        if (gridPoints != null)
        {
            foreach (GameObject a in gridPoints)
            {
                if (a != null)
                {
                    Destroy(a);
                }
            }
        }
       
    }

    public float spacingReturn()
    {
        return spacingOffsetX;
    }

    public void SetOrigin(Vector3 StartRoom)
    {
        gridOrigin = StartRoom;
    }
}
