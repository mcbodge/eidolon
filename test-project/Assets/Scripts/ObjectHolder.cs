using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour {

	private GameObject player;
    private Rigidbody rigidBody;
    private bool isHeld = false;
	private int actionID;
	private ActionHelper actionManager;

	// Use this for initialization
	void Start () {
		actionID = -1;
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
		actionManager = ActionHelper.GetManager();
	}

    void Update ()
    {
        if (isHeld)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                ((player.transform.position + player.transform.up * 1f) // this takes a position below camera
                + player.transform.forward * 1.5f) + player.transform.right * 0.3f,
                Time.deltaTime * 5f
            );
			transform.rotation = player.transform.rotation * Quaternion.AngleAxis(-90f, Vector3.right);

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
		CanvasControl.CanvasControlRef.SetObjectInHand (gameObject.name);
		isHeld = true;
		rigidBody.isKinematic = true;
		// disable the child hotspot
		// transform.getchild gets the transform of the Hotspot
		// .gameobject gets the gameobject associated to the Hotspot transform
		// that is the hotspot itself
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		if (param > 0) {
			actionID = param;
			actionManager.Dispatcher (actionID, "grab");
			actionManager.ObjectInHand = this.gameObject;
		} else {
			actionManager.ObjectInHand = null;
		}
	}

    public void UnGrab()
    {
		CanvasControl.CanvasControlRef.SetDefaultObjectInHand ();
        isHeld = false;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
		if (actionID > 0) {
			actionManager.Dispatcher (actionID, "ungrab");
		}
    }

    public void Launch()
    {
		CanvasControl.CanvasControlRef.SetDefaultObjectInHand ();
		isHeld = false;
		rigidBody.useGravity = true;
		rigidBody.isKinematic = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
		rigidBody.AddForce (- transform.up * 7f, ForceMode.Impulse);
		if (actionID > 0) {
			actionManager.Dispatcher (actionID, "launch");
		}
    }

	public void Drop() {
		CanvasControl.CanvasControlRef.SetDefaultObjectInHand ();
		isHeld = false;
		rigidBody.useGravity = true;
		rigidBody.isKinematic = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
	}
	
}
