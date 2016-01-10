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
    private bool isKetchupBrokenWithBear;

	public void Start() {
		actionHelperRef = ActionHelper.GetManager ();
        isKetchupBrokenWithBear = false;
}

	void OnTriggerEnter(Collider other) {
        if (isKetchupBrokenWithBear && 
                   other.name.Equals("104Peter")) {
            actionHelperRef.RunOutroLevelZero();
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Ketchup") &&
            !actionHelperRef.HasObjectInHand && 
            !isKetchupBrokenWithBear)
        {
            isKetchupBrokenWithBear = true;
            actionHelperRef.PutObjectInFloorHotSpot();
        }
    }
}
