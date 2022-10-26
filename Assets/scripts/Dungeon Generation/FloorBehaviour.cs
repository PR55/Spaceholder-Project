using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    public float spacing;

    public ChildCollider childCollider;
    

    public float GetSpace()
    {
        return spacing;
    }

    public void buildComplete()
    {
        childCollider.gameObject.SetActive(false);
    }

    public bool collideCheck()
    {
        return childCollider.collideCheck();
    }

    public GameObject BuildRoom(bool[] directions, Vector3 parentTransform, GameObject roomToBuild, Transform roomParent, bool isEndRoom)
    {
        GameObject builtRoom = null;
        if (directions[0])
        {
            builtRoom = Instantiate(roomToBuild, new Vector3(this.transform.position.x - ((roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f)), parentTransform.y, parentTransform.z), Quaternion.identity);
        }
        else if (directions[1])
        {
            builtRoom = Instantiate(roomToBuild, new Vector3(this.transform.position.x + ((roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f)), parentTransform.y, parentTransform.z), Quaternion.identity);
        }
        else if (directions[2])
        {
            builtRoom = Instantiate(roomToBuild, new Vector3(parentTransform.x, parentTransform.y, this.transform.position.z + ((roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f))), Quaternion.identity);
        }
        else if (directions[3])
        {
            builtRoom = Instantiate(roomToBuild, new Vector3(parentTransform.x, parentTransform.y, this.transform.position.z - ((roomToBuild.GetComponent<RoomAttribute>().RoomDimensions().y * .55f))), Quaternion.identity);
        }

        if(!isEndRoom)
        builtRoom.transform.parent = roomParent;

        return builtRoom;
    }

}
