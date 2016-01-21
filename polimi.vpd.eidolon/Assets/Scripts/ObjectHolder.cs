using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour
{

    public GameObject player;
    
	private Rigidbody rigidBody;
    private bool isHeld = false;
    private int actionID;
    private ActionHelper actionManager;
    private Vector3 startPosition;
    private Quaternion startRotation;

    // Use this for initialization
    void Start()
    {
        actionID = -1;
        rigidBody = GetComponent<Rigidbody>();
        actionManager = ActionHelper.GetManager();
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }

    void Update()
    {
        if (isHeld)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                ((player.transform.position + player.transform.up * 1.5f) // this takes a position below camera
                + player.transform.forward * 1.2f) + player.transform.right * 0.3f,
                Time.deltaTime * 6f
            );
            transform.rotation = player.transform.rotation * Quaternion.AngleAxis(-90f, Vector3.right);

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
        GetComponent<Collider>().enabled = false;
        rigidBody.isKinematic = true;
        // get the hotspot component from the children of the main object
		// and disable it
		GetComponentInChildren<AC.Hotspot>().gameObject.SetActive(false);
        if (param > 0)
        {
            actionID = param;
            actionManager.Dispatcher(actionID, Action.Grab);
            actionManager.ObjectInHand = this.gameObject;
			actionManager.HasObjectInHand = true;
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
        GetComponent<Collider>().enabled = true;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        GetComponentsInChildren<AC.Hotspot>(true)[0].gameObject.SetActive(true);
		actionManager.HasObjectInHand = false;
    }

    public void ResetObjectPosition()
    {
        Drop();
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
    }

}
