using UnityEngine;
using System.Collections;
using AC;
using System.Collections.Generic;

public class ActionHelperLevel1 : ActionHelper
{

    public List<AC.Hotspot> hotspots;

    private static ActionHelperLevel1 actionHelperReference;

    public void Start()
    {
        gameOverMenu = PlayerMenus.GetMenuWithName("GameOver");
		AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
    }

    /*
        TODO: define objects ids
    */
    public override void Dispatcher(int param, Action sender)
    {
        
    }

    public void DisableAllHotspots()
    {
        foreach (AC.Hotspot hs in hotspots)
        {
            hs.gameObject.SetActive(false);
        }
    }

    public void EnableAllHotspots()
    {
        foreach (AC.Hotspot hs in hotspots)
        {
            hs.gameObject.SetActive(true);
        }
    }
}
