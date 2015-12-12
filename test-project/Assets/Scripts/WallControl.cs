using UnityEngine;
using System.Collections;

public class WallControl : MonoBehaviour {

    public GameObject Player;

    public void Face()
    {
        Ray ray = new Ray(Player.transform.position, Player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 4))
        {
            Debug.Log(hit.collider.gameObject.name);
            Player.transform.LookAt(hit.collider.gameObject.transform);
        }
    }
}
