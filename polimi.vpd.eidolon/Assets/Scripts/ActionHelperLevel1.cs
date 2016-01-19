using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;

public class ActionHelperLevel1 : ActionHelper
{
    private static ActionHelperLevel1 actionHelperReference;

    public void Start()
    {
        gameOverMenu = PlayerMenus.GetMenuWithName("GameOverLvl1");
		AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
    }

    /*
        TODO: define objects ids
    */
    public override void Dispatcher(int param, Action sender)
    {
        
    }

    public void OpenGameOverMenu()
    {
        gameOverMenu.TurnOn();
    }
}
