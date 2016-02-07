using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;
using System;

public class ActionHelperLevel0 : ActionHelper
{
    public bool isFirstObjectPlaced;
    public GameObject Doll;

    // Hotspots
    //public GameObject TeddyBearHotspot;
    //public GameObject KetchupHotspot;
    //public GameObject RcCarHotspot;
    public GameObject Character106Hotspot;
    
    public Cutscene LevelZeroCutscene;
    public Cutscene ObjectPlacingFeedbackCutscene;
    public Cutscene ObjectPlacingMiddleFeedbackCutscene;
    public Cutscene ObjectPlacingLastFeedbackCutscene;
    public Cutscene TakeObjectCS;

    // these are Ketchup/Dool/Car gameobjects
    public List<GameObject> PlacedObjects;
    public List<GameObject> BeerHotspots;

    // these are Left boxes gameobjects
    private List<GameObject> triggersQueue;
    private int BeerNumber;

    public void Start()
    {
        PlacedObjects = new List<GameObject>(2);
        triggersQueue = new List<GameObject>();
        isFirstObjectPlaced = false;
        BeerNumber = 0;
        AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
    }

    // Queue helpers

    public GameObject GetElement(int index)
    {
        return (triggersQueue.Count > 0) ? triggersQueue[index] : null;
    }

    public void NextBeer()
    {
        BeerNumber++;
    }

    public void AddTriggerToQueue(GameObject latest)
    {
        triggersQueue.Add(latest);
    }

    public void RemoveTriggerFromQueue(GameObject item)
    {
        triggersQueue.Remove(item);
    }

    public List<GameObject> GetQueue()
    {
        return triggersQueue;
    }

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
                ObjectAction(sender);
                break;
            case 3:
                ObjectAction(sender);
                break;
        }
    }

    private void ObjectAction(Action sender)
    {
        switch (sender)
        {
            case Action.Grab:
                RemoveObjectFromFloorHotspot();
                break;
            case Action.Ungrab:
                PutObjectInFloorHotSpot();
                break;
            case Action.Launch:
                break;
        }
    }

    public void RemoveObjectFromFloorHotspot()
    {
        GameObject trig = ObjectInHand.transform.FindChild("LeftBox").gameObject;
        if (RoomWithPlayer == Room.Corridor)
        {
            RemoveTriggerFromQueue(trig);
            GameObject t = GetElement(0);
            trig.SetActive(false);
            if (t == null) //significa che era il primo
            {
                isFirstObjectPlaced = false;
            } else
            {
                if (t.activeSelf == false)
                {
                    t.SetActive(true);
                    PlacedObjects.Clear();
                }
                if (PlacedObjects.Contains(ObjectInHand))
                {
                    PlacedObjects.Remove(ObjectInHand);
                }
            }
            Debug.Log("ACTIONHELPER: isFirstObjectPlace=" + isFirstObjectPlaced.ToString());
            DebugLists();
        }
        if (triggersQueue.Count == 0)
        {
            TakeObjectCS.Interact();
        }
    }

    public void PutObjectInFloorHotSpot()
    {
        GameObject trig = ObjectInHand.transform.FindChild("LeftBox").gameObject;
        if (RoomWithPlayer == Room.Corridor &&
             !isFirstObjectPlaced)
        {
            AddTriggerToQueue(trig);
            trig.SetActive(true);
            RunPlayerFeedback();
            isFirstObjectPlaced = true;
            Debug.Log("ACTIONHELPER: isFirstObjectPlace=" + isFirstObjectPlaced.ToString());
        } else if (RoomWithPlayer == Room.Corridor &&
             isFirstObjectPlaced)
        {
            AddTriggerToQueue(trig);
            Debug.Log("THERE ARE ALREADY OTHER ITEMS INTO LIST");
            if (ObjectInHand.name.Equals("Ketchup") &&
                triggersQueue.Contains(Doll.transform.FindChild("LeftBox").gameObject))
            {
                ChangeTextureToDoll();
            }
        }
    }

    public void ChangeTextureToDoll()
    {
        Doll.GetComponent<TextureControl>().ChangeMainTextureToTarget();
    }

    // Helpers

    public void DisableCutsceneHotspots()
    {
        foreach (GameObject hotspot in CutsceneHotspots)
        {
            hotspot.SetActive(false);
        }
        if (BeerNumber < 3)
            BeerHotspots[BeerNumber].SetActive(false);
    }

    public void EnableCutsceneHotspots()
    {
        foreach (GameObject hotspot in CutsceneHotspots)
        {
            hotspot.SetActive(true);
        }
        if (BeerNumber < 3)
            BeerHotspots[BeerNumber].SetActive(true);
    }


    public void RunOutroLevelZero()
    {
        KickStarter.stateHandler.gameState = GameState.Normal;
        LevelZeroCutscene.Interact();
    }

    public void DebugLists()
    {
        string output1 = "";
        foreach ( GameObject g in triggersQueue)
        {
            output1 += g.transform.parent.name + " + ";
        }
        Debug.Log("ACTIONHELPER: triggersQueue: " + output1);
        output1 = "";
        foreach (GameObject g in PlacedObjects)
        {
            output1 += g.name + " + ";
        }
        Debug.Log("ACTIONHELPER: PlacedObjects: " + output1);
    }

}
