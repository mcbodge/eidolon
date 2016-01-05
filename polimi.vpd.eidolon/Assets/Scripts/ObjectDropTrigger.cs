using UnityEngine;
using System.Collections;

public class ObjectDropTrigger : MonoBehaviour {

	private ActionHelper actionHelperRef;

	public void Start() {
		actionHelperRef = ActionHelper.GetManager ();
	}

	void OnTriggerEnter(Collider other) {
		if (!actionHelperRef.isTeddyBearInPosition) {
			// we first check only for bear.
			// Once it is in position we should
			// switch checked object to ketchup
			CheckBear (other);
		} else {
			CheckBottle (other);
		}
	}

	private void CheckBear(Collider other) {
		if (other.name.Equals ("TeddyBear") == true 
		) {
			actionHelperRef.PutObjectInFloorHotSpot ();
			actionHelperRef.FloorHotspot.SetActive (false);
		}
	}

	private void CheckBottle(Collider other) {
		if (other.name.Equals ("Ketchup") == true 
		) {
			actionHelperRef.PutObjectInFloorHotSpot ();
			actionHelperRef.FloorHotspot.SetActive (false);
		}
	}
}
