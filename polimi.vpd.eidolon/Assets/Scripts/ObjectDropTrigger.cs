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
                if (actionHelperRef.GetQueue().Count == 2 && FindObjectInQueue("doll") 
                    && FindObjectInQueue("RCCar"))
                {
                    actionHelperRef.RunMiddlePlayerFeedback();
                } else if (actionHelperRef.GetQueue().Count == 3)
                {
                    actionHelperRef.RunLastPlayerFeedback();
                }
                Debug.Log("OBJECTDROPTRIGGER: added to list " + other.gameObject.name);
            }
        }

        // End game
        if ( CheckEndGame(other) )
        {
            actionHelperRef.RunOutroLevelZero();
        }
	}

    public bool CheckEndGame(Collider other)
    {
        List<GameObject> temp;
        if ( other.gameObject.name.Equals("Peter") &&
            FindObjectInQueue("doll") &&
            FindObjectInQueue("RCCar"))
        {
            return true;
        }
        return false;
    }

    public bool FindObjectInQueue (string name)
    {
        bool returnValue = false;
        foreach (GameObject g in actionHelperRef.GetQueue())
        {
            if (g.transform.parent.name.Equals(name))
            {
                returnValue = true;
            }
        }
        return returnValue;
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
