using UnityEngine;
using System.Collections;

public enum Direction
{
    North = 1,
    South = 3,
    East = 2,
    West = 4
}

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
				Time.deltaTime * 6f
			);
			checkWalkFinish();
		}
	}

    void FaceDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                break;
            case Direction.East:
                transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                break;
            case Direction.South:
                transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
                break;
            case Direction.West:
                transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
                break;
        }
    }

    public void FaceNorth()
    {
        FaceDirection(Direction.North);
        goToMarker();
    }

    public void FaceEast()
    {
        FaceDirection(Direction.East);
        goToMarker();
    }

    public void FaceSouth()
    {
        FaceDirection(Direction.South);
        goToMarker();
    }

    public void FaceWest()
    {
        FaceDirection(Direction.West);
        goToMarker();
    }

	public void goToMarker() {
		Debug.Log ("Starting walk to animation");
		finalPosition = transform.position + (transform.forward * 3f);
		triggerWalk = true;
		//TODO stop player completely
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	public void checkWalkFinish() {
		if ( Vector3.Distance ( transform.position,
		                       finalPosition ) < 0.1f) {
			triggerWalk = false;
			Debug.Log ("Marker reached");
		}
	}



}
