using UnityEngine;
using System.Collections;

public class ManSight : MonoBehaviour {

	private RaycastHit hit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast (transform.position + transform.forward * 0.5f,
			   transform.forward,
			   out hit,
			   3f)) {

			if (hit.collider.gameObject.name.Equals("Eidolon") &&
				ActionHelper.GetManager ().HasObjectInHand) {
				ActionHelper.GetManager().OpenGameOverMenu();
			}
		}
	}
}
