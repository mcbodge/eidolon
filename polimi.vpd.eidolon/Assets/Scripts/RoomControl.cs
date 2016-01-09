using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomControl : MonoBehaviour {

    public List<Camera> RoomCameras;

    public bool IsPlayerInRoom;

    public ActionHelper sceneHelper;

    public Room ThisRoom;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Eidolon"))
        {
            sceneHelper.RoomWithPlayer = ThisRoom;
        }
    }
}

public enum Room{
    Room104,
    Room105,
    Room106,
    Corridor
}
