using UnityEngine;
using System.Collections;

public class ActionHelper : MonoBehaviour {

	public GameObject TeddyBear;
    public GameObject TeddyBearHotspot;
	public GameObject FloorHotspot;
	public GameObject ObjectInHand;
	public bool TeddyBearInPosition;

    private static ActionHelper actionHelperReference;

    public ActionHelper() {
		if (actionHelperReference == null) {
			actionHelperReference = this;
		}
	}

    public static ActionHelper GetManager()
    {
        return actionHelperReference;
    }

	public void Start() {
		FloorHotspot.SetActive(false);
		TeddyBearInPosition = false;
	}

	public void Dispatcher(int param, string sender) {
		switch (param) {
		case 1:
			TeddyBearAction(sender);
			break;
		case 2:
			KetchupAction(sender);
			break;
		default:
			break;
		}
	}

	private void TeddyBearAction( string sender ) {
		switch (sender) {
		case "grab":
            // Activate hotspot
            FloorHotspot.SetActive(true);
			Debug.Log ("Activating hotspot - State:" + FloorHotspot.activeSelf.ToString());
			TeddyBearInPosition = false;
			break;
		case "ungrab":
		case "launch":
			// Deactivate hotspot
			FloorHotspot.SetActive(false);
			Debug.Log ("Deactivating hotspot - State:" + FloorHotspot.activeSelf.ToString());
			break;
		}
	}

	private void KetchupAction(string sender) {

		if (sender == "grab" && TeddyBearInPosition) {
			// Activate hotspot
			FloorHotspot.SetActive (true);
			Debug.Log ("Activating hotspot - State:" + FloorHotspot.activeSelf.ToString ());
            TeddyBearHotspot.SetActive(false);
		} else {
            // Deactivate hotspot
            FloorHotspot.SetActive (false);
			Debug.Log ("Deactivating hotspot - State:" + FloorHotspot.activeSelf.ToString ());
            TeddyBearHotspot.SetActive(true);
        }
	}

	public void putObjectInFloorHS () {
		ObjectHolder objectHolderReference = ((ObjectHolder)ObjectInHand.GetComponent<ObjectHolder> ());
		if (!TeddyBearInPosition) {
			FloorHotspot.SetActive (false);
			objectHolderReference.Drop ();
			Debug.Log ("Dropping object " + objectHolderReference.name);
			ObjectInHand.transform.position = FloorHotspot.transform.position + 0.3f * transform.up;
			TeddyBearInPosition = true;
		} else {
			if (ObjectInHand.name == "Ketchup") {
				Debug.Log ("changing texture to Bear with the bloody one");
                TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
                // Re-set the hotspots
                FloorHotspot.SetActive(false);
                TeddyBearHotspot.SetActive(true);
            }
			Debug.Log ("Dropping object " + objectHolderReference.name + " after TeddyBear");
			objectHolderReference.Drop();
			ObjectInHand.transform.position = (FloorHotspot.transform.position + 0.3f * transform.up)
				+ 0.2f * transform.right;
		}
	}
}
