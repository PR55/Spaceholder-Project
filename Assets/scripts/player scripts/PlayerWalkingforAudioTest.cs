using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingforAudioTest : MonoBehaviour
{
    public Vector3 force;

    public Vector3 collisionForce;

    public Vector3 thrustForce;
    

    bool hitGorund = false;

    float mouseX;
    float mouseY;

    public float mouseSensitivity = 100f;

    public Transform player;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
        player.Rotate(Vector3.up * mouseY);
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, thrustForce.z * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -1 * thrustForce.z * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {

            this.gameObject.transform.Translate(new Vector3(-1 * thrustForce.z * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {

            this.gameObject.transform.Translate(new Vector3(thrustForce.z * Time.deltaTime, 0, 0));
        }
    }

    
}
