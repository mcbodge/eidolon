using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
	public Canvas Canvas;
    public List<Sprite> Images;

	private Image imageBox;
    private int imageShown;
    private bool tutorialEnabled;

    void Start()
    {
        imageShown = 0;
		imageBox = Canvas.GetComponentInChildren<Image> ();
        SetTutorialCanvas(false);
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

    }

	/*
	 * Function to control the tutorial screen on canvas	
	 */
    public void AddImageToCanvas()
    {
        if (imageShown < Images.Count)
        {
            imageBox.sprite = Images[imageShown];
            imageShown++;
        } else
        {
            SetTutorialCanvas(false);
            imageShown = 0;
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

}
