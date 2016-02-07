using UnityEngine;
using AC;
using System.Collections;
using System;

public class ObjectInHandDetection : MonoBehaviour
{

    private RaycastHit hit;
    private ActionHelper actionManager;
    private Camera currentCamera;
    private bool gameOverStarted;

    public Room CurrentRoom;
    public Cutscene CaughtCutscene;

    // Use this for initialization
    void Start()
    {
        actionManager = ActionHelper.GetManager();
        currentCamera = gameObject.GetComponent<Camera>();
        gameOverStarted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

//#if UNITY_EDITOR
//        if (actionManager.RoomWithPlayer == CurrentRoom && actionManager.HasObjectInHand)
//        {
//            Debug.DrawRay(transform.position, actionManager.ObjectInHand.transform.position - transform.position);
//            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(currentCamera), actionManager.ObjectInHand.GetComponent<Renderer>().bounds))
//            {
//                Debug.Log("Frustum view true");
//                if (Physics.Raycast(transform.position, actionManager.ObjectInHand.transform.position - transform.position, out hit))
//                {
//                    Debug.Log("Raycast hit true");
//                    if (hit.collider.gameObject.name.Equals("Eidolon") || hit.collider.gameObject.name.Equals(actionManager.ObjectInHand.name))
//                    {
//                        GameOver();
//                    }
//                }
//            }
//        }

        // These conditions must be checked in the given order
        if ( !gameOverStarted &&
            // 0. If the player is in the current room
            actionManager.RoomWithPlayer == CurrentRoom &&
            // 1. The player has an object in his/her hand
            actionManager.HasObjectInHand &&
            // 2. The object is in the frustum of the current camera
            GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(currentCamera), actionManager.ObjectInHand.GetComponent<Renderer>().bounds) &&
            // 3. Get the first collider between the camera and the object
            Physics.Raycast(transform.position, actionManager.ObjectInHand.transform.position - transform.position, out hit) &&
            // 4. Check if the object (or the player) is interceptable
            (hit.collider.gameObject.name.Equals("Eidolon") || hit.collider.gameObject.name.Equals(actionManager.ObjectInHand.name))
            )
        {
            gameOverStarted = true;
            GameOver();
        }

    }


    private void GameOver()
    {
        Invoke("FinallyGameOver", 4.9f);
        CaughtCutscene.InteractWithActionCamera(gameObject.GetComponent<_Camera>());
    }

    private void FinallyGameOver()
    {
        actionManager.Player.GetComponent<PlayerInteractions>().Respawn();
        gameOverStarted = false;
    }

    public void SetCurrentRoom(Room room)
    {
        CurrentRoom = room;
    }
}
