using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccessNextLevel : MonoBehaviour
{
    public float activateDistance = 3;
    public GameObject parent;

    RoomGeneration roomGeneration;
    GameObject player;

    IOCcam iOCcam;

    // Start is called before the first frame update
    private void Awake()
    {
        roomGeneration = FindObjectOfType<RoomGeneration>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>() != null)
        {
            iOCcam = GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>();
        }

    }

    public void NextLevel()
    {
        if(parent != null && Vector3.Distance(this.gameObject.transform.position, player.transform.position) < activateDistance && roomGeneration.EndRoomCurrent() == parent)
        {
            
            if(roomGeneration != null)
            roomGeneration.NextLevel(roomGeneration.EndRoomCurrent());

            if (iOCcam != null)
                iOCcam.Setup();

            Destroy(this.gameObject);
        }
    }
}
