using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;

public enum Action
{
    Grab,
    Ungrab,
    Launch 
}

public class ActionHelper : MonoBehaviour
{

    public GameObject TeddyBear;
    public GameObject ObjectInHand;

    // Hotspots
    public GameObject TeddyBearHotspot;
    public GameObject KetchupHotspot;
    public GameObject ShowerHotspot;
    public GameObject BeerHotspot;
    public GameObject Character106Hotspot;

    public List<GameObject> CutsceneHotspots;

    public Room RoomWithPlayer;

    public bool HasObjectInHand;
    public Cutscene LevelZeroCutscene;
	public Cutscene ObjectPlacingFeedbackCutscene;
    public Cutscene ObjectPlacingLastFeedbackCutscene;
    public bool IsKetchupInPosition;

    private static ActionHelper actionHelperReference;
	private Menu gameOverMenu;

    public ActionHelper()
    {
        actionHelperReference = this;
    }

    public static ActionHelper GetManager()
    {
        return actionHelperReference;
    }

    public void Start()
    {
        gameOverMenu = PlayerMenus.GetMenuWithName("GameOver");
        IsKetchupInPosition = false;
        ObjectInHand = null;
        HasObjectInHand = false;
    }

    public void Dispatcher(int param, Action sender)
    {
        switch (param)
        {
            case 1:
                TeddyBearAction(sender);
                break;
            case 2:
                break;
        }
    }

    private void TeddyBearAction(Action sender)
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
        ObjectHolder objectHolderReference = ((ObjectHolder)ObjectInHand.GetComponent<ObjectHolder>());
        if (ObjectInHand.name == "TeddyBear")
        {
            if (RoomWithPlayer.Equals(Room.Corridor))
            {
                RunPlayerFeedback();
            }
            Debug.LogFormat("Dropping object {0} in T statement", objectHolderReference.name);
        }
        else if (ObjectInHand.name == "Ketchup")
        {
            RunLastPlayerFeedback();
            IsKetchupInPosition = true;
            KetchupHotspot.SetActive(false);
            TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
            Debug.LogFormat("Dropping object {0} in K statement", objectHolderReference.name);
        }
    }

	// Helpers

    public void DisableCutsceneHotspots ()
    {
        foreach(GameObject hotspot in CutsceneHotspots)
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

	public void OpenGameOverMenu() {
		gameOverMenu.TurnOn ();
	}

    public void RunOutroLevelZero()
    {
        KickStarter.stateHandler.gameState = GameState.Normal;
        LevelZeroCutscene.Interact();
    }

	private void RunPlayerFeedback()
	{
		ObjectPlacingFeedbackCutscene.Interact ();
	}

    private void RunLastPlayerFeedback()
    {
        ObjectPlacingLastFeedbackCutscene.Interact();
    }
}
