using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour {

    GameObject fpsCamera;
    Rigidbody rb;
    bool isHeld = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        fpsCamera = GameObject.FindWithTag("FpsCamera");
	}

	/*
	 *  TO AVOID COLLISION WE CAN'T ACTIVATE COLLISION AND COLLIDERS ON OBJECT
	 * BECAUSE ISKINEMATIC DISABLES ALL COLLISIONS
	 * 
	 * WE SHOULD ACTIVATE SOME INVISIBLE OBJECT ON eidolon wich lets us stop near
	 * obstacles
	 * */

    void Update ()
    {
        if (isHeld)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                (fpsCamera.transform.position +  - fpsCamera.transform.up * 0.5f) // this takes a position below camera
                + fpsCamera.transform.forward * 1.5f,
                Time.deltaTime * 5f
            );
			transform.rotation = fpsCamera.transform.rotation * Quaternion.AngleAxis(-90f, Vector3.right);

			// ungrab object if f key is pressed
            if (Input.GetKeyDown("f"))
            {
                UnGrab();

                this.isHeld = false;
            }
			if (Input.GetKeyDown("r"))
			{
				Launch();
				
				this.isHeld = false;
			}
        }
    }

    public void Grab ()
    {
		isHeld = true;
		rb.isKinematic = true;
    }

    public void UnGrab()
    {
        this.isHeld = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void Launch()
    {
		this.isHeld = false;
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.AddForce (- transform.up * 7f, ForceMode.Impulse);
    }

}
