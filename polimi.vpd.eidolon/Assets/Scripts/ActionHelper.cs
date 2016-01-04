using UnityEngine;
using System.Collections;
using AC;

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
    public GameObject FloorHotspot;
    public GameObject ShowerHotspot;
    public GameObject BeerHotspot;
    public GameObject Character106Hotspot;


    public bool HasObjectInHand;
    public Cutscene LevelZeroCutscene;

    private static ActionHelper actionHelperReference;
    private bool isTeddyBearInPosition;

    public ActionHelper()
    {
        if (actionHelperReference == null)
        {
            actionHelperReference = this;
        }
    }

    public static ActionHelper GetManager()
    {
        return actionHelperReference;
    }

    public void Start()
    {
        FloorHotspot.SetActive(false);
        isTeddyBearInPosition = false;
    }

    public void Dispatcher(int param, Action sender)
    {
        switch (param)
        {
            case 1:
                TeddyBearAction(sender);
                break;
            case 2:
                KetchupAction(sender);
                break;
        }
    }

    private void TeddyBearAction(Action sender)
    {
        switch (sender)
        {
            case Action.Grab:
                // Activate hotspot
                FloorHotspot.SetActive(true);
                Debug.LogFormat("Activating hotspot - State: {0}", FloorHotspot.activeSelf);
                isTeddyBearInPosition = false;
                break;
            case Action.Ungrab:
            case Action.Launch:
                // Deactivate hotspot
                FloorHotspot.SetActive(false);
                Debug.LogFormat("Deactivating hotspot - State: {0}", FloorHotspot.activeSelf);
                break;
        }
    }

    private void KetchupAction(Action sender)
    {

        if (sender == Action.Grab && isTeddyBearInPosition)
        {
            // Activate hotspot
            FloorHotspot.SetActive(true);
            Debug.LogFormat("Activating hotspot - State: {0}", FloorHotspot.activeSelf);
            TeddyBearHotspot.SetActive(false);
        }
        else
        {
            // Deactivate hotspot
            FloorHotspot.SetActive(false);
            Debug.LogFormat("Deactivating hotspot - State: {0}", FloorHotspot.activeSelf);
            TeddyBearHotspot.SetActive(true);
        }
    }

    public void PutObjectInFloorHotSpot()
    {
        ObjectHolder objectHolderReference = ((ObjectHolder)ObjectInHand.GetComponent<ObjectHolder>());
        objectHolderReference.Drop();
        if (ObjectInHand.name == "TeddyBear")
        {
            isTeddyBearInPosition = true;
            Debug.LogFormat("Dropping object {0} in T statement", objectHolderReference.name);
        }
        else if (ObjectInHand.name == "Ketchup")
        {
            TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
            Invoke("RunOutroLevelZero", 5f);
            // Re-set the hotspots
            // Disabled by ketchup's grab
            TeddyBearHotspot.SetActive(true);
            KetchupHotspot.SetActive(false);
            Debug.LogFormat("Dropping object {0} in K statement", objectHolderReference.name);
        }
        FloorHotspot.SetActive(false);
    }

    public void DisableCutsceneHotspots ()
    {
        ShowerHotspot.SetActive(false);
        BeerHotspot.SetActive(false);
        Character106Hotspot.SetActive(false);
    }

    public void EnableCutsceneHotspots()
    {
        ShowerHotspot.SetActive(true);
        BeerHotspot.SetActive(true);
        Character106Hotspot.SetActive(true);
    }

    private void RunOutroLevelZero()
    {
        KickStarter.stateHandler.gameState = GameState.Normal;
        LevelZeroCutscene.Interact();
    }
}
