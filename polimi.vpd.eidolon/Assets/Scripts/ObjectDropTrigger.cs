using UnityEngine;
using System.Collections;

public class ObjectDropTrigger : MonoBehaviour {

	private bool isBearInPosition;
	private ActionHelper actionHelperRef;
	private string objectToCheck;

	public void Start() {
		isBearInPosition = false;
		objectToCheck = "TeddyBear";
		actionHelperRef = ActionHelper.GetManager ();
	}

	void OnTriggerEnter(Collider other) {
		if (!isBearInPosition) {
			// we first check only for bear
			// once it is in position we should
			// switch checked object to ketchup
			CheckBear (other);
		} else {
			CheckBottle (other);
		}
	}

	void CheckBear(Collider other) {
		if (other.name.Equals ("TeddyBear") == true 
		) {
			isBearInPosition = true;
			actionHelperRef.PutObjectInFloorHotSpot ();
			actionHelperRef.FloorHotspot.SetActive (false);
		}
	}

	void CheckBottle(Collider other) {
		if (other.name.Equals ("Ketchup") == true 
		) {
			actionHelperRef.PutObjectInFloorHotSpot ();
			actionHelperRef.FloorHotspot.SetActive (false);
		}
	}

	/* No need of this right now
	void OnTriggerExit(Collider other) { 
		if (other.name.Equals ("ObjectLeftBox") == true &&
			name.Equals ("TeddyBear") == true) {
		}
	} */
}
