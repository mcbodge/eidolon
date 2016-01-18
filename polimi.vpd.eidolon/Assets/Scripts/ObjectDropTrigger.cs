using UnityEngine;
using System.Collections;

public class ObjectDropTrigger : MonoBehaviour {


	private ActionHelperLevel0 actionHelperRef;
    private bool isKetchupReady;
    private bool isRcCarReady;

	public void Start() {
		actionHelperRef = ActionHelper.GetManager () as ActionHelperLevel0;
        isKetchupReady = false;
        isRcCarReady = false;
}

	void OnTriggerEnter(Collider other) {
        if (isRcCarReady &&
                   other.name.Equals("Peter")) {
            actionHelperRef.RunOutroLevelZero();
        }
	}

    void OnTriggerStay(Collider other)
    {
        // If player drops ketchup we check if the player
        // isn't grabbing anything else: this means that he has dropped it
        if (other.name.Equals("Ketchup") &&
            !actionHelperRef.HasObjectInHand && 
            !isKetchupReady)
        {
            isKetchupReady = true;
            CheckHotspotIsFull();
            actionHelperRef.PutObjectInFloorHotSpot();
        }
        // if player drops car 
        if (other.name.Equals("RCcar") &&
            !actionHelperRef.HasObjectInHand &&
            !isRcCarReady)
        {
            isRcCarReady = true;
            CheckHotspotIsFull();
            actionHelperRef.PutObjectInFloorHotSpot();
        }
    }

    private void CheckHotspotIsFull()
    {
        if (isRcCarReady)
        {
            // this means the Theatre on the floor is ready
            // so say last phrase
            actionHelperRef.RunLastPlayerFeedback();
        } else
        {
            // the Theatre on floor is not finished
            actionHelperRef.RunMiddlePlayerFeedback();
        }
    }
}
