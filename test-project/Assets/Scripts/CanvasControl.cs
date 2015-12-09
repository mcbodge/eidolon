using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

    //TODO improve
    public Text TestText;
    private int count;

    void Start()
    {
        SetDefaultMessage();
        count = 0;
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
