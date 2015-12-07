using UnityEngine;
using System.Collections;

public class ActionHelper : MonoBehaviour {

	public GameObject Orso;
	public Texture bloodyTexture;
	public GameObject floorHotspot;
	public static  ActionHelper HelperReference;
	public GameObject ObjectInHand;
	public bool orsacchiottoInPosition;

	public ActionHelper() {
		if (HelperReference == null) {
			HelperReference = this;
		}
	}

	public void Start() {
		floorHotspot.SetActive(false);
		orsacchiottoInPosition = false;
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
			//attivare hotspot
			floorHotspot.SetActive(true);
			Debug.Log ("Activating hotspot - State:" + floorHotspot.activeSelf.ToString());
			orsacchiottoInPosition = false;
			break;
		case "ungrab":
		case "launch":
			//disattivare hotspot
			floorHotspot.SetActive(false);
			Debug.Log ("Deactivating hotspot - State:" + floorHotspot.activeSelf.ToString());
			break;
		}
	}

	private void KetchupAction(string sender) {

		if (sender == "grab" && orsacchiottoInPosition) {
			//attivare hotspot
			floorHotspot.SetActive (true);
			Debug.Log ("Activating hotspot - State:" + floorHotspot.activeSelf.ToString ());
		} else {
			//disattivare hotspot
			floorHotspot.SetActive (false);
			Debug.Log ("Deactivating hotspot - State:" + floorHotspot.activeSelf.ToString ());
		}
	
	}

	public void putObjectInFloorHS () {
		ObjectHolder ohReference = ((ObjectHolder)ObjectInHand.GetComponent<ObjectHolder> ());
		if (!orsacchiottoInPosition) {
			floorHotspot.SetActive (false);
			ohReference.Drop ();
			Debug.Log ("Dropping object " + ohReference.name);
			ObjectInHand.transform.position = floorHotspot.transform.position + 0.3f * transform.up;
			orsacchiottoInPosition = true;
		} else {
			if (ObjectInHand.name == "Ketchup") {
				Debug.Log ("changing texture to Bear with the bloody one");
				Orso.GetComponent<Renderer>().material.SetTexture("_MainTex", bloodyTexture);
			}
			Debug.Log ("Dropping object " + ohReference.name + " after TeddyBear");
			ohReference.Drop();
			ObjectInHand.transform.position = (floorHotspot.transform.position + 0.3f * transform.up)
				+ 0.2f * transform.right;
		}
	}
}
