using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipEnemy : MonoBehaviour
{

    public GameObject minimapIcon;
    public float showTime = 2f;
    public float hideBuffer = 1f;
    
    bool called = false;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    private void Update()
    {
        if(!called)
        {
            Invoke("Show", showTime);
            Invoke("Hide", showTime + hideBuffer);
            called = true;
        }
    }
    void Hide()
    {
        minimapIcon.SetActive(false);
        called = false;
    }
    void Show()
    {
        minimapIcon.SetActive(true);
    }
}
