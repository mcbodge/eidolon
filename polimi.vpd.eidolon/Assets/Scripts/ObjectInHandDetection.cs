using UnityEngine;
using System.Collections;

public class ObjectInHandDetection : MonoBehaviour {

	private RaycastHit hit;
    private ActionHelper actionManager;
    private Camera currentCamera;

    // Use this for initialization
    void Start () {
        actionManager = ActionHelper.GetManager();
        currentCamera = gameObject.GetComponent<Camera>();
    }

	// Update is called once per frame
	void FixedUpdate () {
#if UNITY_EDITOR
        if (actionManager.HasObjectInHand)
        {
            Debug.DrawRay(transform.position, actionManager.ObjectInHand.transform.position - transform.position);
            if(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(currentCamera), actionManager.ObjectInHand.GetComponent<Renderer>().bounds)){
                Debug.Log("Frustum view true");
                if(Physics.Raycast(transform.position, actionManager.ObjectInHand.transform.position - transform.position, out hit))
                {
                    Debug.Log("Raycast hit true");
                    if(hit.collider.gameObject.name.Equals("Eidolon") || hit.collider.gameObject.name.Equals(actionManager.ObjectInHand.name)){
                        actionManager.OpenGameOverMenu();
                    }
                }
            }
        }
#endif

#if UNITY_STANDALONE
        // Different compilation model and nested computation for performance purpose

        // These conditions must be checked in the given order
        if (
            // 1. The player has an object in his/her hand
            actionManager.HasObjectInHand &&
            // 2. The object is in the frustum of the current camera
            GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(currentCamera), actionManager.ObjectInHand.GetComponent<Renderer>().bounds) &&
            // 3. Get the first collider between the camera and the object
            Physics.Raycast(transform.position, actionManager.ObjectInHand.transform.position - transform.position, out hit) &&
            // 4. Check if the object (or the player) is interceptable
            (hit.collider.gameObject.name.Equals("Eidolon") || hit.collider.gameObject.name.Equals(actionManager.ObjectInHand.name))
            )
        {
            actionManager.OpenGameOverMenu();
        }
#endif
    }
}
