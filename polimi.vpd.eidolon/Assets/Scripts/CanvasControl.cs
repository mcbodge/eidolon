using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public List<Sprite> FirstSlot;
    public List<Sprite> SecondSlot;
    public List<Sprite> ThirdSlot;
    public List<Sprite> FourthSlot;
    public Sprite HintImage;

    private Image imageBox;
    private bool isManagerBusy;
    private bool showingHintImage;

    void Start()
    {
        imageBox = gameObject.GetComponentInChildren<Image>();
        isManagerBusy = false;
        showingHintImage = false;
        imageBox.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isManagerBusy )
        {
            AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Never;
            EnableHintImage();
        }
        else if (Input.GetKeyUp(KeyCode.I) && showingHintImage)
        {
            DisableHintImage();
            AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Always;
        }
    }

    public void EnableFirstSlot()
    {
        EnableSlot(FirstSlot);
    }

    #region [ Private methods ]
    private void EnableHintImage()
    {
        imageBox.enabled = showingHintImage = true;
        imageBox.sprite = HintImage;
    }

    private void DisableHintImage()
    {
        imageBox.enabled = showingHintImage = false;
    }

    private void EnableSlot(List<Sprite> specificSlot)
    {
        AC.KickStarter.cursorManager.cursorDisplay = AC.CursorDisplay.Never;
        if (showingHintImage)
            DisableHintImage();
        if (!isManagerBusy)
            StartCoroutine(ManageSlot(specificSlot));
#if UNITY_EDITOR
        else
            Debug.LogWarning("Canvas is busy! New Coroutine not launched.");
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
    #endregion
}
