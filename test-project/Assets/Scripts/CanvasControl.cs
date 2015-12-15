using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

    public Text TopLeftMessage;
	public Text TopRightMessage;
    public Image ImageBox;
    public List<Sprite> Images;

    private int imageShown;
    private bool tutorialEnabled;
    private static CanvasControl canvasControlReference;

    public CanvasControl () 
	{
		if (canvasControlReference == null) {
			canvasControlReference = this;
		}
	}

    void Start()
    {
        SetDefaultMessage();
        imageShown = 0;
        tutorialEnabled = false;
        ImageBox.enabled = tutorialEnabled;
    }

    void Update()
    {
        if (tutorialEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                AddImageToCanvas();
            }
        }

    }

    public static CanvasControl GetManager()
    {
        return canvasControlReference;
    }

#if UNITY_EDITOR
    public void SetObjectInHand(string holdedObject) {
		TopRightMessage.text = string.Format("Holding {0}", holdedObject);
	}

	public void SetDefaultObjectInHand() {
		TopRightMessage.text = "No object in hand";
	}
#endif

    public void SetTopLeftMessage(string newText)
    {
        TopLeftMessage.text = newText;
    }

    public void SetCaughtMessage ()
    {
        TopLeftMessage.text = string.Format("!!!");
    }

    public void SetDefaultMessage()
    {
        TopLeftMessage.text = "";
    }

    public void AddImageToCanvas()
    {
        if (imageShown < Images.Count)
        {
            ImageBox.sprite = Images[imageShown];
            imageShown++;
        } else
        {
            ImageBox.enabled = false;
            tutorialEnabled = false;
            imageShown = 0;
        }
            
    }

    public void TutorialEnable()
    {
        ImageBox.enabled = true;
        tutorialEnabled = true;
        AddImageToCanvas();
    }
    

}
