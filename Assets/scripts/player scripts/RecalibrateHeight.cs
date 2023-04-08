using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecalibrateHeight : MonoBehaviour
{

    [SerializeField]
    private float defaultHeight = 2.0f;
    [SerializeField]
    private Camera camera;

    public Text timerDisplay;
    public Image radialTime;

    public float recalibrateTime = 4;

    float timer = 0;

    bool calledOnce;

    public GameObject mainTabMenu;
    public GameObject recalibrateMenu;

    private void Start()
    {
        calledOnce = true;
        recalibrateMenu.SetActive(false);
        mainTabMenu.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            timerDisplay.text = Mathf.Floor(timer).ToString();
            radialTime.fillAmount = timer - Mathf.Floor(timer);

        }
        else if(timer <= 0 && !calledOnce)
        {
            Resize();
        }
    }


    void Resize()
    {
        calledOnce = true;
        float headHeight = camera.transform.localPosition.y;
        float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
        recalibrateMenu.SetActive(false);
        mainTabMenu.SetActive(true);
    }

    public void editorResize()
    {
        timer = recalibrateTime;
        calledOnce = false;
        recalibrateMenu.SetActive(true);
        mainTabMenu.SetActive(false);
    }
}
