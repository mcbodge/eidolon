using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour
{

    public GameObject player;
    
	private Rigidbody rigidBody;
    private bool isHeld = false;
    private int actionID;
    private ActionHelper actionManager;

    // Use this for initialization
    void Start()
    {
        actionID = -1;
        rigidBody = GetComponent<Rigidbody>();
        actionManager = ActionHelper.GetManager();
        //SetHasObjectInHand(false);
    }

    void Update()
    {
        if (isHeld)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                ((player.transform.position + player.transform.up * 1.5f) // this takes a position below camera
                + player.transform.forward * 1.8f) + player.transform.right * 0.3f,
                Time.deltaTime * 6f
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

    public void Grab(int param)
    {
        isHeld = true;
        rigidBody.isKinematic = true;
        // disable the child hotspot
        // transform.getchild gets the transform of the Hotspot
        // .gameobject gets the gameobject associated to the Hotspot transform
        // that is the hotspot itself
		GetComponentInChildren<AC.Hotspot>().gameObject.SetActive(false);
        if (param > 0)
        {
            actionID = param;
            actionManager.Dispatcher(actionID, Action.Grab);
            actionManager.ObjectInHand = this.gameObject;
			// here we delay the floor hs activation
			// else we will not be able to pick up the object anymore
			// because the trigger will istantly bring it on floor again
			Invoke ("EnableFloorHS", 1f);
            //SetHasObjectInHand(true);
        }
        else
        {
            actionManager.ObjectInHand = null;
        }
    }

    public void UnGrab()
    {
        Drop();
        if (actionID > 0)
        {
            actionManager.Dispatcher(actionID, Action.Ungrab);
        }
    }

    public void Launch()
    {
        Drop();
        rigidBody.AddForce(-transform.up * 7f, ForceMode.Impulse);
        if (actionID > 0)
        {
            actionManager.Dispatcher(actionID, Action.Launch);
        }
    }

    public void Drop()
    {
        isHeld = false;
        //SetHasObjectInHand(false);
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        GetComponentsInChildren<AC.Hotspot>(true)[0].gameObject.SetActive(true);
    }

   /* private void SetHasObjectInHand(bool value)
    {
        ActionHelper.GetManager().HasObjectInHand = value;
    } */

	private void EnableFloorHS() {
		actionManager.FloorHotspot.SetActive (true);
	}

}
