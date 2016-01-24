using UnityEngine;
using System.Collections;
using System;

public enum Status : int
{
    Start = 0,
    FirstSlotShowed = 5,
    DirectionKeyTextShowed = 10,
    _FlushBoxWithDirectionKeyTextPressed = 12,
    DirectionKeyTextPressed = 15,
    FirstCutsceneShowed = 20,
    SpaceKeyShowed = 25,
    _FlushBoxWithSpaceKeyTextPressed = 27,
    SpaceKeyTextPressed = 30,
    _InBathroom = 32,
    SecondSlotShowed = 35,
    LeftClickTextShowed = 40,
    HasPeterBeenTouched = 45,
    _FlushBoxWithLeftClickTextRemoved = 47,
    LeftClickTextRemoved = 50,
    ThirdSlotShowed = 55,
    CameraShakingTextShowed = 64,
    ClosetReached = 65,
    _FlushBoxWithCameraShakingTextRemoved = 67,
    CameraShakingTextRemoved = 70,
    ReachPeterTextShowed = 75,
    PeterReached = 80,
    _FlushBoxWithReachPeterTextRemoved = 82,
    ReachPeterTextRemoved = 85,
    FourthSlotShowed = 90,
    _InteractSecondCutscene = 92,
    SecondCutsceneShowed = 95,
    SceneKeyShowed = 100,
    _InteractThirdCutscene = 102,
    InteractWithThirdCutscene = 105,
    ThirdCutsceneShowed = 110,
    End = 115
}

public class ActionHelperTutorial : ActionHelper
{
    public Status TutorialStatus;

    public bool EnableHintImage;

    public AC.Cutscene FirstCutscene;

    public AC.Cutscene SecondCutscene;

    public AC.Cutscene ThirdCutscene;

    public AC.Cutscene FourthCutscene;

    public GameObject Room104Trigger;

    public GameObject PeterHotspot;
    
    private bool isClosetReachedCalled;

    void Start()
    {
        TutorialStatus = Status.Start;
        Room104Trigger.SetActive(false);
        isClosetReachedCalled = false;
    }

    public void SetStatus(int correspondentValue)
    {
        TutorialStatus = (Status)correspondentValue;
    }

    public void ClosetReached()
    {
        if (TutorialStatus.Equals(Status.CameraShakingTextShowed) && !isClosetReachedCalled)
        {
            isClosetReachedCalled = true;
            TutorialStatus = Status.ClosetReached;
        }
    }
}
