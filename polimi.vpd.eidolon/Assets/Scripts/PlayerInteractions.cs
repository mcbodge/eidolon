using UnityEngine;
using System.Collections;

public enum Direction
{
    North = 1,
    South = 3,
    East = 2,
    West = 4
}

public class PlayerInteractions : MonoBehaviour
{

    private bool triggerWalk;
    private Vector3 finalPosition;

    // Use this for initialization
    void Start()
    {
        triggerWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerWalk)
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                finalPosition,
                Time.deltaTime * 6f
            );
            CheckWalkFinish();
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

    public void MoveNorth()
    {
        FaceDirection(Direction.North);
        MoveForward();
    }

    public void MoveEast()
    {
        FaceDirection(Direction.East);
        MoveForward();
    }

    public void MoveSouth()
    {
        FaceDirection(Direction.South);
        MoveForward();
    }

    public void MoveWest()
    {
        FaceDirection(Direction.West);
        MoveForward();
    }

    private void MoveForward()
    {
        Debug.Log("Starting walk to animation");
        finalPosition = transform.position + (transform.forward * 3f);
        triggerWalk = true;
        //TODO stop player completely
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void CheckWalkFinish()
    {
        if (Vector3.Distance(transform.position, finalPosition) < 0.1f)
        {
            triggerWalk = false;
            Debug.Log("Marker reached");
        }
    }



}
