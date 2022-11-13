using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasShow : MonoBehaviour
{
    public Canvas canvasArm;

    public float[] ranges;

    // Start is called before the first frame update
    void Start()
    {
        canvasArm.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(this.gameObject.transform.eulerAngles.z > 360)
        {
            if(ranges[0]-360 > this.gameObject.transform.eulerAngles.z - 360&& this.gameObject.transform.eulerAngles.z -360 > ranges[1] - 360)
            {
                canvasArm.gameObject.SetActive(true);
            }
            else
            {
                canvasArm.gameObject.SetActive(false);
            }
        }
        else
        {
            if (ranges[0] < this.gameObject.transform.eulerAngles.z && this.gameObject.transform.eulerAngles.z < ranges[1])
            {
                canvasArm.gameObject.SetActive(true);
            }
            else
            {
                canvasArm.gameObject.SetActive(false);
            }
        }    
    }

}
