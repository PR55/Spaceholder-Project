using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChildCollider : MonoBehaviour
{
    BoxCollider boxCollider;

    bool isCollide = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetBounds(Vector3 bounds)
    {
        if(boxCollider != null)
        boxCollider.size = bounds;
    }

    public bool CollideCheck()
    {
        return isCollide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != this.gameObject)
            isCollide = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != this.gameObject)
            isCollide = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != this.gameObject)
            isCollide = false;
    }
}
