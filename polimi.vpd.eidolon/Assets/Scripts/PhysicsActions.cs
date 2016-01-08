using UnityEngine;
using System.Collections;

public class PhysicsActions : MonoBehaviour {

    public Transform playerTransform;

    public void KnockOut()
    {
        // the impulse force goes from the player to te object
        gameObject.GetComponent<Rigidbody>().AddForce(-(playerTransform.position - transform.position), ForceMode.Impulse);
    }
}
