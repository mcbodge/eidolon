using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;
using System;

public class ActionHelperLevel0 : ActionHelper
{
    public bool isFirstObjectPlaced;
    public GameObject TeddyBear;

    // Hotspots
    public GameObject TeddyBearHotspot;
    public GameObject KetchupHotspot;
    public GameObject RcCarHotspot;
    public GameObject Character106Hotspot;
    
    public Cutscene LevelZeroCutscene;
    public Cutscene ObjectPlacingFeedbackCutscene;
    public Cutscene ObjectPlacingMiddleFeedbackCutscene;
    public Cutscene ObjectPlacingLastFeedbackCutscene;

    // these are Ketchup/Dool/Car gameobjects
    public List<GameObject> PlacedObjects;
    

    // these are Left boxes gameobjects
    private List<GameObject> triggersQueue;

    public void Start()
    {
        PlacedObjects = new List<GameObject>(2);
        triggersQueue = new List<GameObject>();
        isFirstObjectPlaced = false;
    }

    // Queue helpers

    public GameObject GetFirstTrigger()
    {
        return (triggersQueue.Count > 0) ? triggersQueue[0] : null;
    }

    public void AddTriggerToQueue(GameObject latest)
    {
        triggersQueue.Add(latest);
    }

    public void RemoveTriggerFromQueue(GameObject item)
    {
        triggersQueue.Remove(item);
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
            GameObject t = GetFirstTrigger();
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
            Debug.Log("ACTIONHELPER: isFirstObjectPlace=" + isFirstObjectPlaced.ToString());
            // if in hand we have ketchup and doll is on ground , doll should be
            // ketchupped
            if (ObjectInHand.name.Equals("Ketchup") &&
                PlacedObjects.Contains(TeddyBear))
            {
                ChangeTextureToDoll();
            }
        }
    }

    public void ChangeTextureToDoll()
    {
        TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
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

    public void DisableMainObjectsHS()
    {
        TeddyBearHotspot.SetActive(false);
        KetchupHotspot.SetActive(false);
        RcCarHotspot.SetActive(false);
    }

    public void EnableMainObjectsHS()
    {
        TeddyBearHotspot.SetActive(true);
        KetchupHotspot.SetActive(true);
        RcCarHotspot.SetActive(true);
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
