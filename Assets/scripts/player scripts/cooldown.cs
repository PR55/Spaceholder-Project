using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    public PlayerStats access;
    public Image Cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;

    // Update is called once per frame
    void Update()
    {
        
            //Reduce fill amount over 30 seconds
            Cooldown.fillAmount = access.CurrentHealthCanvas();
        
    }
}
