using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour {

    GameObject fpsCamera;
    Rigidbody rb;
    bool isHeld = false;
	int actionID;
	ActionHelper ahReference;

	// Use this for initialization
	void Start () {
		actionID = -1;
        rb = GetComponent<Rigidbody>();
        fpsCamera = GameObject.FindWithTag("FpsCamera");
		ahReference = ActionHelper.HelperReference;
	}

    void Update ()
    {
        if (isHeld)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                (fpsCamera.transform.position +  - fpsCamera.transform.up * 0.4f) // this takes a position below camera
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
	
	public void Grab (int param)
	{	
		Debug.Log("Object launching Grab() has param=" + param.ToString());
		isHeld = true;
		rb.isKinematic = true;
		if (param > 0) {
			actionID = param;
			ahReference.Dispatcher (actionID, "grab");
			ahReference.ObjectInHand = this.gameObject;
			Debug.Log ("Object in hand set to " + ahReference.ObjectInHand.name);
		} else {
			ahReference.ObjectInHand = null;
			Debug.Log ("No object in hand");
		}
	}

    public void UnGrab()
    {
        this.isHeld = false;
        rb.useGravity = true;
        rb.isKinematic = false;
		if (actionID > 0) {
			ahReference.Dispatcher (actionID, "ungrab");
		}
    }

    public void Launch()
    {
		this.isHeld = false;
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.AddForce (- transform.up * 7f, ForceMode.Impulse);
		if (actionID > 0) {
			ahReference.Dispatcher (actionID, "launch");
		}
    }

	public void Drop() {
		isHeld = false;
		rb.useGravity = true;
		rb.isKinematic = false;
	}
	
}
