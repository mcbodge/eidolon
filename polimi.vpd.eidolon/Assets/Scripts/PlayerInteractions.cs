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
    public GameObject RespawnReference;
    public ActionHelper SceneHelper;

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
                Time.deltaTime * 4f
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
        Debug.Log("Start walking through wall");
        finalPosition = transform.position + (transform.forward * 2.5f);
        triggerWalk = true;
    }

    public void CheckWalkFinish()
    {
        if (Vector3.Distance(transform.position, finalPosition) < 0.25f)
        {
            triggerWalk = false;
            Debug.Log("Reached other side");
        }
    }

    public void Respawn()
    {
        if (SceneHelper.HasObjectInHand)
            SceneHelper.ObjectInHand.GetComponent<ObjectHolder>().ResetObjectPosition();
        StartCoroutine(MoveResource(gameObject.transform, RespawnReference.transform.position, 0.4f));
    }

    private static IEnumerator MoveResource(Transform resourceTransform, Vector3 endPosition, float speed)
    {
        Vector3 startPosition = resourceTransform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            resourceTransform.position = Vector3.Lerp(startPosition, endPosition, t);
            if (Vector3.Distance(resourceTransform.position, endPosition) < 0.4f)
                t = 1f;
            yield return 0;
        }
        ActionHelper.GetManager().EnableClosetTriggers();
    }
}
