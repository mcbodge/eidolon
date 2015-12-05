using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour {

    GameObject fpsCamera;
    Rigidbody rb;
    bool isHeld = false;
	int actionID;

	// Use this for initialization
	void Start () {
		actionID = -1;
        rb = GetComponent<Rigidbody>();
        fpsCamera = GameObject.FindWithTag("FpsCamera");
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
		Debug.Log(param.ToString());
		isHeld = true;
		rb.isKinematic = true;
		if (param > 0) {
			actionID = param;
			ActionHelper.HelperReference.Dispatcher (actionID, "grab");
			ActionHelper.HelperReference.ObjectInHand = this.gameObject;
			Debug.Log ("Object in hand: " + ActionHelper.HelperReference.ObjectInHand.name);
		} else {
			ActionHelper.HelperReference.ObjectInHand = null;
			Debug.Log ("none");
		}
	}

    public void UnGrab()
    {
        this.isHeld = false;
        rb.useGravity = true;
        rb.isKinematic = false;
		if (actionID > 0) {
			Debug.Log("UnGrab with param");
			ActionHelper.HelperReference.Dispatcher (actionID, "ungrab");
		}
    }

    public void Launch()
    {
		this.isHeld = false;
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.AddForce (- transform.up * 7f, ForceMode.Impulse);
		if (actionID > 0) {
			Debug.Log("Launch with param");
			ActionHelper.HelperReference.Dispatcher (actionID, "ungrab");
		}
    }

	public void Drop() {
		isHeld = false;
		rb.useGravity = true;
		rb.isKinematic = false;
	}
	
}
