using UnityEngine;
using System.Collections;

public class PhysicsActions : MonoBehaviour {

    public void KnockOut()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * 1f, ForceMode.Impulse);
    }
}
