using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVHealthdecrease : MonoBehaviour
{
    public PlayerStats access;

    public Material m;

    public bool tabletInArm = false;

    public float decreaseRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //controls the overall exposure weight by comparing the health to see when it increases and decreases 
        //float healthExposure = access.CurrentHealth();
        //if (m.GetFloat("_EmissiveExposureWeight") < healthExposure && tabletInArm) 
        //{
        //    m.SetFloat("_EmissiveExposureWeight", healthExposure);
        //    Debug.Log("Stable");
        //}
        //else if (m.GetFloat("_EmissiveExposureWeight") > healthExposure && tabletInArm)
        //{
        //    float curExposure = m.GetFloat("_EmissiveExposureWeight");
        //    m.SetFloat("_EmissiveExposureWeight", curExposure - decreaseRate);
        //    Debug.Log("Decreasing");
        //}
        //else if (m.GetFloat("_EmissiveExposureWeight") > 1f && !tabletInArm)
        //{
        //    m.SetFloat("_EmissiveExposureWeight", 1f);
        //    Debug.Log("Stable");
        //}
        //else if (m.GetFloat("_EmissiveExposureWeight") < 1f && !tabletInArm)
        //{
        //    float curExposure = m.GetFloat("_EmissiveExposureWeight");
        //    m.SetFloat("_EmissiveExposureWeight", curExposure + decreaseRate);
        //    Debug.Log("Increasing");
        //}
        
    }


    //These are used for when entering and exiting a socket so it knows when a tablet is in a socket
    public void TabletInArm()
    {
        tabletInArm = true;
    }

    public void TabletOutOfArm()
    {
        tabletInArm = false;
    }

}
