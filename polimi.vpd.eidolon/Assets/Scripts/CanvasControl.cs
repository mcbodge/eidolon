using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public List<Sprite> Images;

	private Image imageBox;
    private bool tutorialEnabled;

    void Start()
    {
		imageBox = gameObject.GetComponentInChildren<Image> ();
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

}
