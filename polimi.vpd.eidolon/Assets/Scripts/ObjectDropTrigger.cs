using UnityEngine;
using System.Collections;

public class ObjectDropTrigger : MonoBehaviour {

	private bool isObjectOnFloorHS;
	private ActionHelper actionHelperRef;

	public void Start() {
		isObjectOnFloorHS = false;
		actionHelperRef = ActionHelper.GetManager ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals ("ObjectLeftBox") == true &&
			( name.Equals ("TeddyBear") == true ||
			  name.Equals("Bottle") ) 
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
