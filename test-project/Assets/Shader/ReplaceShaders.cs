using UnityEngine;
using System.Collections;

public class ReplaceShaders : MonoBehaviour
{

    public static Shader myShader;

    void Start()
    {

        myShader = Shader.Find("Toon");

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject myObj in allObjects)
        {
            if (myObj.GetComponent<Renderer>() && myObj.tag != "NoShader")
            {
                Material[] allMaterials = myObj.GetComponent<Renderer>().materials;
                foreach (Material mat in allMaterials)
                    mat.shader = myShader;
            }
        }
    }
}

