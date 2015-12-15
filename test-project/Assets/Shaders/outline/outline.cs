using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class outline : MonoBehaviour {
  public float intensity;
  private Material material;
  
  // Creates a private material uset to the effect
  void Awake()
  {
    material = new Material(Shader.Find("Hidden/Outline") );
  }
  
  // Postprocess the image
  void OnRenderImage (RenderTexture source, RenderTexture destination)
  {
    if (intensity == 0)
    {
      Graphics.Blit (source, destination);
      return;
    }
    
    material.SetFloat("_bwBlend", intensity);
    Graphics.Blit (source, destination, material);
  }
}
