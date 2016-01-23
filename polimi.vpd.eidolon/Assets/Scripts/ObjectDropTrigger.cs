using UnityEngine;
using System.Collections.Generic;

public class ObjectDropTrigger : MonoBehaviour {

	private ActionHelperLevel0 actionHelperRef;

	public void Start() {
		actionHelperRef = ActionHelper.GetManager () as ActionHelperLevel0;
    }

	void OnTriggerEnter(Collider other) {
        if (CheckEnter(other))
        {
            if (!actionHelperRef.PlacedObjects.Contains(other.gameObject))
            {
                actionHelperRef.PlacedObjects.Add(other.gameObject);
                actionHelperRef.DebugLists();
                if (actionHelperRef.PlacedObjects.Count == 1)
                {
                    actionHelperRef.RunMiddlePlayerFeedback();
                } else if (actionHelperRef.PlacedObjects.Count == 2)
                {
                    actionHelperRef.RunLastPlayerFeedback();
                }
                Debug.Log("OBJECTDROPTRIGGER: added to list " + other.gameObject.name);
            }
        }

        // End game
        if (other.name.Equals("Peter") && actionHelperRef.PlacedObjects.Count == 2)
        {
            actionHelperRef.RunOutroLevelZero();
        }
	}

    public bool CheckEnter(Collider other)
    {
        GameObject parent = transform.parent.gameObject;
        if (!other.gameObject.Equals(parent))
        {
            if (other.name.Equals("doll") || other.name.Equals("Ketchup") ||
            other.name.Equals("RCCar"))
            {
                return true;
            }
        }
            
        return false;
    }

    public override string ToString()
    {
        return gameObject.name;
    }

}
