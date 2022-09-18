using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterHold : MonoBehaviour
{
    public float showDistance = 15f;

    public GameObject player;
    public Image holdFill;
    public float timeToWait = 2f;
    public Transform targetDestination;

    public Transform targetDestinationCheck;

    public GameObject buttonActivate;


    float timeWait = 0f;
    bool isIn = false;


    // Start is called before the first frame update
    void Start()
    {
        if(holdFill != null)
            holdFill.fillAmount = 0;
        isIn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 difference = new Vector3(
  player.transform.position.x - targetDestinationCheck.position.x,
  player.transform.position.y - targetDestinationCheck.position.y,
  player.transform.position.z - targetDestinationCheck.position.z);
        Debug.Log(this.gameObject.name + " Distances : " + difference.ToString());
        
        if (difference.x <= showDistance && difference.x >= -showDistance || difference.z <= showDistance && difference.z >= -showDistance)
        {
            if(!buttonActivate.activeSelf)
                buttonActivate.SetActive(true);
        }
        else
        {
            if (buttonActivate.activeSelf)
                buttonActivate.SetActive(false);
            return;
        }
        if (isIn && timeWait < timeToWait)
        {
            timeWait += Time.fixedDeltaTime;
        }
        else if (timeWait > 0 && !isIn)
        {
            timeWait -= Time.fixedDeltaTime;
        }
        if (holdFill != null)
            holdFill.fillAmount = timeWait/timeToWait;
        if(holdFill.fillAmount >= .99)
        {
            ChangeState();
            player.transform.position = targetDestination.position;
            holdFill.fillAmount = 0f;
            
        }
        Debug.Log(this.gameObject.name + " Fill Amount : " + holdFill.fillAmount.ToString());


        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeState();
            
        }
    }

    public void ChangeState()
    {
        if (isIn)
        {
            isIn = false;
            Debug.Log("Off");
        }
        else if (!isIn)
        {
            isIn = true;
            Debug.Log("On");
        }
    }
}
