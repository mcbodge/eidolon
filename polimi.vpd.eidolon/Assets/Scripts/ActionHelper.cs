using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public enum Action
{
    Grab,
    Ungrab,
    Launch 
}

public class ActionHelper : MonoBehaviour
{
    
    public GameObject ObjectInHand;
    public bool HasObjectInHand;
    public Room RoomWithPlayer;
    public GameObject Player;

    public float WallToggleTime;

    public List<GameObject> CutsceneHotspots;
    public List<GameObject> MainObjectHotspots;
    public List<GameObject> ClosetTriggers;

    protected Menu gameOverMenu;

    private static ActionHelper actionHelperReference;

    public static ActionHelper GetManager()
    {
        return actionHelperReference;
    }

    public ActionHelper()
    {
        actionHelperReference = this;
        ObjectInHand = null;
        HasObjectInHand = false;
        gameOverMenu = PlayerMenus.GetMenuWithName("GameOver");
    }

    /*
        HELPERS
    */
    public virtual void Dispatcher(int param, Action sender) { }

    internal void OpenGameOverMenu()
    {
        gameOverMenu.TurnOn();
    }

    public void DisableClosetTriggers()
    {
        foreach (GameObject tr in ClosetTriggers)
        {
            tr.SetActive(false);
        }
    }

    public void EnableClosetTriggers()
    {
        foreach (GameObject tr in ClosetTriggers)
        {
            tr.SetActive(true);
        }
    }

    public void DisableMainObjectsHS()
    {
        foreach (GameObject obj in MainObjectHotspots)
        {
            obj.SetActive(false);
        }
    }

    public void EnableMainObjectsHS()
    {
        foreach (GameObject obj in MainObjectHotspots)
        {
            obj.SetActive(true);
        }
    }

    //TODO Code refactoring
    public static void ReloadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ReloadScene()
    {
        ReloadCurrentScene();
    }

    public void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1mirrors");
    }

    public void LoadSecondScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SecondScene");
    }
}
