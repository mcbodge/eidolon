using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public List<Sprite> Images;
    public List<Sprite> Controls;

	private Image imageBox;
    private int controlScreensShown;
    private bool tutorialEnabled;
    private bool controlsScreenEnabled;

    void Start()
    {
		imageBox = gameObject.GetComponentInChildren<Image> ();
        SetTutorialCanvas(false);
        controlScreensShown = 0;
		TutorialEnable (); // start frome here cause there isn't intro scene
    }

    void Update()
    {
        if (tutorialEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                AddImageToCanvas();
            }
        }
        if (controlsScreenEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddImageToControlScreen();
            }
        }
    }

	/*
	 * Function to control the tutorial screen on canvas	
	 */
    public void AddImageToCanvas()
    {
        if (ApplicationModel.CanvasStatus.ImagesShown < Images.Count)
        {
            imageBox.sprite = Images[ApplicationModel.CanvasStatus.ImagesShown];
            ApplicationModel.CanvasStatus.ImagesShown++;
        } else
        {
            SetTutorialCanvas(false);
        }
            
    }

    public void TutorialEnable()
    {
        SetTutorialCanvas(true);
        AddImageToCanvas();
    }
    
    private void SetTutorialCanvas(bool isEnabed)
    {
        imageBox.enabled = tutorialEnabled = isEnabed;
    }

    public void ControlScreenEnable()
    {
        SetTutorialCanvasControl(true);
        AddImageToControlScreen();
    }

    public void AddImageToControlScreen()
    {
        if (controlScreensShown < Controls.Count)
        {
            imageBox.sprite = Controls[controlScreensShown];
            controlScreensShown++;
        }
        else
        {
            SetTutorialCanvasControl(false);
            controlScreensShown = 0;
        }
    }



    private void SetTutorialCanvasControl(bool isEnabed)
    {
        imageBox.enabled = controlsScreenEnabled = isEnabed;
    }

}
