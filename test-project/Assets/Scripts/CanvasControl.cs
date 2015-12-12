using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

    public Text TopLeftMessage;
	public Text TopRightMessage;

    private int count;
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
        count = 0;
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
        count++;
        TopLeftMessage.text = string.Format("!!! #{0}", count);
    }

    public void SetDefaultMessage()
    {
        TopLeftMessage.text = "-";
    }

}
