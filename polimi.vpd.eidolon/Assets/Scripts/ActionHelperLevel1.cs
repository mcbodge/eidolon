using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;

public class ActionHelperLevel1 : ActionHelper
{

    public List<GameObject> PlacedObjects;
    public Cutscene ObjectPlacingCS;
    public Cutscene RightPlaceCS;
    public Canvas canvas;

    public void Start()
    {
		AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
        PlacedObjects = new List<GameObject>();
    }

    /*
        TODO: define objects ids
    */
    public override void Dispatcher(int param, Action sender)
    {
        switch(sender)
        {
            case Action.Grab:
                if (PlacedObjects.Contains(ObjectInHand))
                {
                    PlacedObjects.Remove(ObjectInHand);
                }
                break;
            case Action.Ungrab:
                break;
        }
    }

    public void DisableAllHotspots()
    {
        foreach (GameObject hs in CutsceneHotspots)
        {
            hs.SetActive(false);
        }
    }

    public void EnableAllHotspots()
    {
        foreach (GameObject hs in CutsceneHotspots)
        {
            hs.SetActive(true);
        }
    }

    public void RunPlacingCutscene()
    {
        ObjectPlacingCS.Interact();
    }
}
