using UnityEngine;
using System.Collections;

public class TextureControl : MonoBehaviour {

    public Texture TargetTexture;
    private Texture defaultTexture;
    private const string mainTextureName = "_MainTex";

    public void Start()
    {
        defaultTexture = gameObject.GetComponent<Renderer>().material.GetTexture(mainTextureName);
    }


	public void ChangeMainTexture(Texture inputTexture)
    {
        gameObject.GetComponent<Renderer>().material.SetTexture(mainTextureName, inputTexture);
    }

    public void ChangeMainTextureToTarget()
    {
        ChangeMainTexture(TargetTexture);
    }

    public void ChangeMainTextureToDefault()
    {
        ChangeMainTexture(defaultTexture);
    }
}
