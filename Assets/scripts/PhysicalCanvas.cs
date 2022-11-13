using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalCanvas : MonoBehaviour
{
    Button canvasButton;

    // Start is called before the first frame update
    void Start()
    {
        canvasButton = GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightHand")
        {
            canvasButton.onClick.Invoke();
        }
    }

}
