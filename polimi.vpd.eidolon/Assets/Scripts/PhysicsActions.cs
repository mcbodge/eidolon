using UnityEngine;
using System.Collections;

public class PhysicsActions : MonoBehaviour {

    public Transform Source;

    public void KnockOut()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        Rigidbody gameObjectRigidBody = gameObject.AddComponent<Rigidbody>();
        gameObjectRigidBody.mass = 1;

        // the impulse force goes from the player to te object
        gameObjectRigidBody.AddForce(transform.position - Source.position, ForceMode.Impulse);
    }
}