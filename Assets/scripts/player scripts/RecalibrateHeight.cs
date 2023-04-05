using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecalibrateHeight : MonoBehaviour
{

    [SerializeField]
    private float defaultHeight = 2.0f;
    [SerializeField]
    private Camera camera;

    void Resize()
    {
        float headHeight = camera.transform.localPosition.y;
        float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
    }

    public void editorResize()
    {
        Resize();
    }
}
