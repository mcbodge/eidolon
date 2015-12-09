using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public GameObject markerWalkTo;

	bool triggerWalk;

	// Use this for initialization
	void Start () {
		triggerWalk = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (triggerWalk) {
			transform.position = Vector3.Lerp
			(
				transform.position,
				markerWalkTo.transform.position,
				Time.deltaTime * 3f
			);
			checkAnimationFinish();
		}
	}

	public void goToMarker() {
		Debug.Log ("Starting walk to animation");
		triggerWalk = true;
		// this to stop the player completely
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	public void checkAnimationFinish() {
		if ( Vector3.Distance ( transform.position,
		                       markerWalkTo.transform.position ) < 0.3f) {
			triggerWalk = false;
			Debug.Log ("Marker reached");
		}
	}
}
