using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generaterooms : MonoBehaviour
{

    public Vector2 range = new Vector2(20, 20);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(range.x / 4, 0, range.y / 4), new Vector3(range.x/2,2,range.y/2));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(range.x / -4, 0, range.y / 4), new Vector3(range.x / 2, 2, range.y / 2));
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(range.x / -4, 0, range.y / -4), new Vector3(range.x / 2, 2, range.y / 2));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(range.x / 4, 0, range.y / -4), new Vector3(range.x / 2, 2, range.y / 2));
        
    }
}
