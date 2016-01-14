using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomControl : MonoBehaviour
{

    public List<Camera> RoomCameras;

    public bool IsPlayerInRoom;

    public ActionHelper sceneHelper;

    public Room ThisRoom;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Eidolon"))
        {
            sceneHelper.RoomWithPlayer = ThisRoom;
            Debug.Log(ThisRoom);
        }
    }
}

public enum Room : int
{
    Room104 = 104,
    Room105 = 105,
    Room106 = 106,
    Corridor = 0
}
