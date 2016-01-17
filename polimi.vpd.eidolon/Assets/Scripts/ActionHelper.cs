using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;
using System;

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
    public virtual void Dispatcher(int param, Action sender)
    {

    }

    internal void OpenGameOverMenu()
    {
        gameOverMenu.TurnOn();
    }
}
