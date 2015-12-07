using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

    void Start()
    {
        SetDefaultMessage();
    }

    //TODO improve
    public Text TestText;

    public void SetTestText(string newText)
    {
        TestText.text = newText;
    }

    public void SetCaughtMessage ()
    {
        TestText.text = "!!!";
    }

    public void SetDefaultMessage()
    {
        TestText.text = "-";
    }

}
