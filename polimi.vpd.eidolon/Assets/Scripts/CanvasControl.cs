using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
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

	/*
	 * Getter for the static reference
	 */ 

    public static CanvasControl GetManager()
    {
        return canvasControlReference;
    }

	/*
	 * Function to control the tutorial screen on canvas	
	 */

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
