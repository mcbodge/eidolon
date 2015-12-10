using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

	public static CanvasControl CanvasControlRef;

    //TODO improve
    public Text TestText;
	public Text objectInHand;
    private int count;

	public CanvasControl () 
	{
		if (CanvasControlRef == null) {
			CanvasControlRef = this;
		}
	}

    void Start()
    {
        SetDefaultMessage();
        count = 0;
    }

	public void SetObjectInHand(string ObjectHolded) {
		objectInHand.text = "Holding " + ObjectHolded;
	}

	public void SetDefaultObjectInHand() {
		objectInHand.text = "No object in hand";
	}

    public void SetTestText(string newText)
    {
        TestText.text = newText;
    }

    public void SetCaughtMessage ()
    {
        count++;
        TestText.text = string.Format("!!! #{0}", count);
    }

    public void SetDefaultMessage()
    {
        TestText.text = "-";
    }

}
