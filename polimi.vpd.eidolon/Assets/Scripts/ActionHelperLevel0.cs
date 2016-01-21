using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;

public class ActionHelperLevel0 : ActionHelper
{

    public GameObject TeddyBear;
    public GameObject Player;
    public GameObject FinalDestination;

    // Hotspots
    public GameObject TeddyBearHotspot;
    public GameObject KetchupHotspot;
    public GameObject RcCarHotspot;
    public GameObject Character106Hotspot;

    public List<GameObject> CutsceneHotspots;
    
    public Cutscene LevelZeroCutscene;
    public Cutscene ObjectPlacingFeedbackCutscene;
    public Cutscene ObjectPlacingMiddleFeedbackCutscene;
    public Cutscene ObjectPlacingLastFeedbackCutscene;

    public void RunPlayerFeedback()
    {
        ObjectPlacingFeedbackCutscene.Interact();
    }

    public void RunMiddlePlayerFeedback()
    {
        ObjectPlacingMiddleFeedbackCutscene.Interact();
    }

    public void RunLastPlayerFeedback()
    {
        ObjectPlacingLastFeedbackCutscene.Interact();
    }

    public void Start()
    {
        gameOverMenu = PlayerMenus.GetMenuWithName("GameOver");
    }

    /*
        1 Dool (bear)
        2 Ketchup
        3 RC Car
    */
    public override void Dispatcher(int param, Action sender)
    {
        switch (param)
        {
            case 1:
                ObjectAction(sender);
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public override void Respawn(Room parameter)
    {
        switch (parameter)
        {
            case Room.Room104:
                //TODO
                StartCoroutine(MoveResource(Player.transform, FinalDestination.transform.position, 0.5f));
                break;
            case Room.Room105:
                break;
            case Room.Room106:
                break;
        }
    }

    private void ObjectAction(Action sender)
    {
        switch (sender)
        {
            case Action.Grab:
                break;
            case Action.Ungrab:
                PutObjectInFloorHotSpot();
                break;
            case Action.Launch:
                break;
        }
    }

    public void PutObjectInFloorHotSpot()
    {
        if (ObjectInHand.name == "doll")
        {
            if (RoomWithPlayer.Equals(Room.Corridor))
            {
                RunPlayerFeedback();
            }
            Debug.LogFormat("Dropping object {0} in T statement", ObjectInHand.name);
        }
        else if (ObjectInHand.name == "Ketchup")
        {
            KetchupHotspot.SetActive(false);
            TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
            Debug.LogFormat("Dropping object {0} in K statement", ObjectInHand.name);
        }
        else if (ObjectInHand.name == "RCcar")
        {
            RcCarHotspot.SetActive(false);
            Debug.LogFormat("Dropping object {0} in K statement", ObjectInHand.name);
        }
    }

    // Helpers

    public void DisableCutsceneHotspots()
    {
        foreach (GameObject hotspot in CutsceneHotspots)
        {
            hotspot.SetActive(false);
        }
    }

    public void EnableCutsceneHotspots()
    {
        foreach (GameObject hotspot in CutsceneHotspots)
        {
            hotspot.SetActive(true);
        }
    }

    public void RunOutroLevelZero()
    {
        KickStarter.stateHandler.gameState = GameState.Normal;
        LevelZeroCutscene.Interact();
    }

    public static IEnumerator MoveResource(Transform resourceTransform, Vector3 endPosition, float speed)
    {
        var startPosition = resourceTransform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            resourceTransform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return 0;
        }
    }
}
