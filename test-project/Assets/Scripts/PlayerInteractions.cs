using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	bool triggerWalk;
	Vector3 finalPosition;

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
				finalPosition,
				Time.deltaTime * 3f
			);
			checkAnimationFinish();
		}
	}

	public void goToMarker() {
		Debug.Log ("Starting walk to animation");
		finalPosition = 
			transform.position + (transform.forward * 3f);
		triggerWalk = true;
		// this to stop the player completely
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	public void checkAnimationFinish() {
		if ( Vector3.Distance ( transform.position,
		                       finalPosition ) < 0.1f) {
			triggerWalk = false;
			Debug.Log ("Marker reached");
		}
	}
}
