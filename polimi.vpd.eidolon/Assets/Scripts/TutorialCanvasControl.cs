using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialCanvasControl : MonoBehaviour
{
    public List<Sprite> FirstSlot;
    public List<Sprite> SecondSlot;
    public List<Sprite> ThirdSlot;
    public List<Sprite> FourthSlot;
    public List<Sprite> FifthSlot;
    public Sprite HintImage;
    public ActionHelperTutorial SceneManager;
    public Text MiddleBottomBox;
    public Image ImageBox;

    private bool isManagerBusy;
    private bool showingHintImage;

    private const string directionText =
@"Use your mouse and
[W] [S] [A] [D] or [↑] [↓] [←] [→]
to move around";

    private const string spaceText =
@"Use [SPACE] to walk
through walls
Try to enter room number 104";

    private const string clickText =
@"Use
[mouse click]
to touch Peter";

    private const string reflectionText =
@"Your reflection might be seen
Try to go in front of the closet";

    private const string sceneText =
@"Press
[I]
to see the scene to reenact";

    void Start()
    {
        isManagerBusy = false;
        showingHintImage = false;
    }

    void Update()
    {
        if (SceneManager.EnableHintImage)
        {
            if (Input.GetKeyDown(KeyCode.I) && !isManagerBusy)
            {
                AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Never;
                EnableHintImage();
            }
            ////else if (Input.GetKeyUp(KeyCode.I) && showingHintImage)
            ////{
            ////    DisableHintImage();
            ////    AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
            ////}
        }
        if (IsCurrent(Status.FirstSlotShowed)) {
            SetText(directionText);
            SetStatus(Status.DirectionKeyTextShowed);
        }
        else if(IsCurrent(Status.DirectionKeyTextShowed) && MovementKeyPressed())
        {
            SetStatus(Status._FlushBoxWithDirectionKeyTextPressed);
            Invoke("FlushBoxWithDirectionKeyTextPressed", 2f);
        }
        else if (IsCurrent(Status.FirstCutsceneShowed))
        {
            SceneManager.Room104Trigger.SetActive(true);
            SetText(spaceText);
            SetStatus(Status.SpaceKeyShowed);
        }
        else if ((IsCurrent(Status.SpaceKeyShowed) && SceneManager.RoomWithPlayer.Equals(Room.Room104)))
        {
            EnableSlot(SecondSlot, Status.SecondSlotShowed);
            SetStatus(Status._FlushBoxWithSpaceKeyTextPressed);
            Invoke("FlushBoxWithSpaceKeyTextPressed", 0.4f);
        }
        else if (IsCurrent(Status.SpaceKeyTextPressed) && SceneManager.RoomWithPlayer.Equals(Room.Tutorial_Bathroom))
        {
            SetStatus(Status._InBathroom);
        }
        else if(IsCurrent(Status.SecondSlotShowed) && SceneManager.RoomWithPlayer.Equals(Room.Tutorial_Bathroom))
        {
            SetText(clickText);
            SetStatus(Status.LeftClickTextShowed);
        }
        else if (IsCurrent(Status.HasPeterBeenTouched))
        {
            SetStatus(Status._FlushBoxWithLeftClickTextRemoved);
            Invoke("FlushBoxWithLeftClickTextRemoved", 0.4f);
        }
        else if (IsCurrent(Status.ThirdSlotShowed))
        {
            SetText(reflectionText);
            SetStatus(Status.CameraShakingTextShowed);
        }
        else if (IsCurrent(Status.ClosetReached))
        {
            SetStatus(Status._FlushBoxWithCameraShakingTextRemoved);
            Invoke("FlushBoxWithCameraShakingTextRemoved", 1f);
        }
        else if (IsCurrent(Status.CameraShakingTextRemoved))
        {
            SetText("Now reach Peter");
            SetStatus(Status.ReachPeterTextShowed);
        }
        else if (IsCurrent(Status.PeterReached))
        {
            SetStatus(Status._FlushBoxWithReachPeterTextRemoved);
            Invoke("FlushBoxWithReachPeterTextRemoved", 0.8f);
        }
        else if (IsCurrent(Status.FourthSlotShowed))
        {
            SetStatus(Status._InteractSecondCutscene);
            SceneManager.SecondCutscene.Interact();
        }
        else if (IsCurrent(Status.SecondCutsceneShowed))
        {
            SceneManager.EnableHintImage = true;
            SetText(sceneText);
            SetStatus(Status.SceneKeyShowed);
        }
        else if (IsCurrent(Status.SceneKeyShowed) && Input.GetKeyDown(KeyCode.I))
        {
            FlushBoxAndExecuteActions(Status.InteractWithThirdCutscene);
            SceneManager.ThirdCutscene.Interact();
        }
        else if (IsCurrent(Status.ThirdCutsceneShowed))
        {
            SetStatus(Status.End);
            SceneManager.FourthCutscene.Interact();
        }
    }

    public void EnableFirstSlot()
    {
        EnableSlot(FirstSlot, Status.FirstSlotShowed);
    }

    #region[ Helper methods]

    private bool IsCurrent(Status statusToCheck)
    {
        return SceneManager.TutorialStatus.Equals(statusToCheck);
    }

    private void SetStatus(Status statusToSet)
    {
        SceneManager.TutorialStatus = statusToSet;
    }

    private void SetText(string textToSet)
    {
        MiddleBottomBox.text = textToSet;
    }

    private bool MovementKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private void FlushBoxAndExecuteActions(Status tutorialStatusToSetInExit)
    {
        MiddleBottomBox.text = string.Empty;
        SetStatus(tutorialStatusToSetInExit);
        switch (tutorialStatusToSetInExit)
        {
            case Status.DirectionKeyTextPressed:
                SceneManager.FirstCutscene.Interact();
                break;
            case Status.LeftClickTextRemoved:
                EnableSlot(ThirdSlot, Status.ThirdSlotShowed);
                break;
            case Status.ReachPeterTextRemoved:
                EnableSlot(FourthSlot, Status.FourthSlotShowed);
                break;
            default:
                break;
        }
    }
    #endregion

    #region [ Common private methods ]
    private void EnableHintImage()
    {
        ImageBox.enabled = showingHintImage = true;
        ImageBox.sprite = HintImage;
    }

    private void DisableHintImage()
    {
        ImageBox.enabled = showingHintImage = false;
    }

    private void EnableSlot(List<Sprite> specificSlot, Status tutorialStatusToSetInExit)
    {
        AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Never;
        if (showingHintImage)
            DisableHintImage();
        if (!isManagerBusy)
        {
            StartCoroutine(ManageSlot(specificSlot, tutorialStatusToSetInExit));
        }
#if UNITY_EDITOR
        else
            Debug.LogWarning("Canvas is busy! New Coroutine not launched.");
#endif
    }

    private IEnumerator ManageSlot(List<Sprite> currentSlot, Status tutorialStatusToSetInExit)
    {
        ImageBox.enabled = true;
        ImageBox.sprite = currentSlot[0];
        int currentIndex = 1;
        while (currentIndex <= currentSlot.Count)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentIndex < currentSlot.Count)
                {
                    //AC.KickStarter.stateHandler.gameState = AC.GameState.Paused;
                    ImageBox.sprite = currentSlot[currentIndex];
                }
                else
                {
                    //AC.KickStarter.stateHandler.gameState = AC.GameState.Normal;
                    ImageBox.enabled = false;
                    AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
                    SetStatus(tutorialStatusToSetInExit);
                    if (currentSlot[0].name.Equals("3"))
                    {
                        SceneManager.EnableClosetTriggers();
                    }
                }
                currentIndex++;
            }
            yield return null;
        }
    }
    #endregion

    #region [ Flush callbacks ]

    private void FlushBoxWithReachPeterTextRemoved()
    {
        FlushBoxAndExecuteActions(Status.ReachPeterTextRemoved);
    }

    private void FlushBoxWithCameraShakingTextRemoved()
    {
        FlushBoxAndExecuteActions(Status.CameraShakingTextRemoved);
    }

    private void FlushBoxWithLeftClickTextRemoved()
    {
        FlushBoxAndExecuteActions(Status.LeftClickTextRemoved);
    }

    private void FlushBoxWithDirectionKeyTextPressed()
    {
        FlushBoxAndExecuteActions(Status.DirectionKeyTextPressed);
    }

    private void FlushBoxWithSpaceKeyTextPressed()
    {
        FlushBoxAndExecuteActions(Status.SpaceKeyTextPressed);
        
    }
    #endregion

}
