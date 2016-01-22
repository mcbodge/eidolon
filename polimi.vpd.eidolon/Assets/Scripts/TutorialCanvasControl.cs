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

    private Image imageBox;
    private KeyCode keyCodeToNextPicture;

    private bool isManagerBusy;

    void Start()
    {
        //isFirstSlotEnabled = isSecondSlotEnabled = isThirdSlotEnabled = isFourthSlotEnabled = false;
        imageBox = gameObject.GetComponentInChildren<Image>();
        isManagerBusy = false;
        EnableSlot(FirstSlot);
    }

    private void EnableSlot(List<Sprite> specificSlot)
    {
        AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Never;
        if (!isManagerBusy)
            StartCoroutine(ManageSlot(specificSlot));
#if UNITY_EDITOR
        else
            Debug.LogWarning("Canvas manager is busy!");
#endif
    }

    private IEnumerator ManageSlot(List<Sprite> currentSlot)
    {
        imageBox.enabled = true;
        imageBox.sprite = currentSlot[0];
        int currentIndex = 1;
        while (currentIndex <= currentSlot.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentIndex < currentSlot.Count)
                    imageBox.sprite = currentSlot[currentIndex];
                else
                    imageBox.enabled = false;
                currentIndex++;
            }
            yield return null;
        }
        AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
    }
}