using UnityEngine;
using System.Collections;

public class WallControl : MonoBehaviour {

    public GameObject PlayerObj;

    public void Face()
    {
        PlayerObj.transform.LookAt(new Vector3(gameObject.transform.position.x, PlayerObj.transform.position.y, gameObject.transform.position.z));
    }
}
