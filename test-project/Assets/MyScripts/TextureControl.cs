using UnityEngine;
using System.Collections;

public class TextureControl : MonoBehaviour {

    public Texture TargetTexture;
    private Texture defaultTexture;

    public void Start()
    {
        //TODO test
        defaultTexture = gameObject.GetComponent<Renderer>().material.GetTexture("_MainTex");
    }


	public void ChangeMainTexture(Texture inputTexture)
    {
        gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", inputTexture);
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
