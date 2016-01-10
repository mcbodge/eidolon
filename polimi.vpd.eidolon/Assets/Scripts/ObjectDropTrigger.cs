using UnityEngine;
using System.Collections;

public class ObjectDropTrigger : MonoBehaviour {

	/* This class is assigned to a hotspot, and it will check for
	 * objects colliding with it. We first check for the bear colliding with the floor
	 * Once it is in position we switch checked object to ketchup.
	 * In case we place first ketchup, it will not be recognized due to the
	 * if clause, which will execute CheckBear(). If we place the bear after the ketchup
	 * the method will execute from now on CheckBottle(), but it will not trigger because
	 * Ketchup is already inside the box, and the method will trigger only on enter.
	 */

	private ActionHelper actionHelperRef;

	public void Start() {
		actionHelperRef = ActionHelper.GetManager ();
	}

	void OnTriggerEnter(Collider other) {
		if (!actionHelperRef.IsTeddyBearInPosition) {
			
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
