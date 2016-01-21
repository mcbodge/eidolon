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
    }

    /*
        TODO: define objects ids
    */
    public virtual void Dispatcher(int param, Action sender) { }

    public virtual void Respawn(Room parameter) { }

    internal void OpenGameOverMenu()
    {
        gameOverMenu.TurnOn();
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

    public void LoadSecondScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SecondScene");
    }
}
