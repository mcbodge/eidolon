using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
	public Canvas canvas;
    public List<Sprite> Images;

	private Image ImageBox;
    private int imageShown;
    private bool tutorialEnabled;

    void Start()
    {
        imageShown = 0;
		ImageBox = canvas.GetComponentInChildren<Image> ();
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
