using UnityEngine;
using System.Collections;

public class ActionHelper : MonoBehaviour
{

    public GameObject TeddyBear;
    public GameObject TeddyBearHotspot;
    public GameObject KetchupHotspot;
    public GameObject FloorHotspot;
    public GameObject ObjectInHand;

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

    public void Dispatcher(int param, string sender)
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

    private void TeddyBearAction(string sender)
    {
        switch (sender)
        {
            case "grab":
                // Activate hotspot
                FloorHotspot.SetActive(true);
                Debug.Log(string.Format("Activating hotspot - State: {0}", FloorHotspot.activeSelf));
                isTeddyBearInPosition = false;
                break;
            case "ungrab":
            case "launch":
                // Deactivate hotspot
                FloorHotspot.SetActive(false);
                Debug.Log(string.Format("Deactivating hotspot - State: {0}", FloorHotspot.activeSelf));
                break;
        }
    }

    private void KetchupAction(string sender)
    {

        if (sender == "grab" && isTeddyBearInPosition)
        {
            // Activate hotspot
            FloorHotspot.SetActive(true);
            Debug.Log(string.Format("Activating hotspot - State: {0}", FloorHotspot.activeSelf));
            TeddyBearHotspot.SetActive(false);
        }
        else
        {
            // Deactivate hotspot
            FloorHotspot.SetActive(false);
            Debug.Log(string.Format("Deactivating hotspot - State: {0}", FloorHotspot.activeSelf));
            TeddyBearHotspot.SetActive(true);
        }
    }

    public void PutObjectInFloorHotSpot()
    {
        ObjectHolder objectHolderReference = ((ObjectHolder)ObjectInHand.GetComponent<ObjectHolder>());
        objectHolderReference.Drop();
        if (ObjectInHand.name == "TeddyBear")
        {
            //TODO Final position of the teddybear (fixed)
            ObjectInHand.transform.position = FloorHotspot.transform.position + 0.3f * transform.up;
            isTeddyBearInPosition = true;
            Debug.Log(string.Format("Dropping object {0} in T statement", objectHolderReference.name));
        }
        else if (ObjectInHand.name == "Ketchup")
        {
            //TODO Final position of ketchup (fixed)
            ObjectInHand.transform.position = (FloorHotspot.transform.position + 0.3f * transform.up) + 0.2f * transform.right;
            TeddyBear.GetComponent<TextureControl>().ChangeMainTextureToTarget();
            // Re-set the hotspots
            // Disabled by ketchup's grab
            TeddyBearHotspot.SetActive(true);
            KetchupHotspot.SetActive(false);
            Debug.Log(string.Format("Dropping object {0} in K statement", objectHolderReference.name));
        }
        FloorHotspot.SetActive(false);
    }
}
