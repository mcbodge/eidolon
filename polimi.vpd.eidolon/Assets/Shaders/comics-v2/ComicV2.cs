using UnityEngine;
using System.Collections;

public class ComicV2 : MonoBehaviour
{

    public Shader Normals;
    public Shader Colored_objects;
    public Shader Outline;
    public float intensity;
    private Material material;

    // parameters
    public float sensitivityDepth = 1.0f;
    public float sensitivityNormals = 1.0f;
    public float sampleDist = 1.0f;
    public float edgesOnly = 0.0f;
    public Color edgesOnlyBgColor = Color.white;

    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject myObj in allObjects)
        {
            Renderer objectRenderer = myObj.GetComponent<Renderer>();

            if (objectRenderer != null && myObj.tag != "Color")
            {
                //Material[] allMaterials = objectRenderer.sharedMaterials;
                Material[] allMaterials = objectRenderer.materials;
                foreach (Material mat in allMaterials)
                {
					if(mat.name != "Particle" && ! mat.name.Contains("mask"))
                        mat.shader = Normals; // a material executes all the passes in the shader
                }
                    
            }
        }
        material = new Material(Outline);
    }
    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_bwBlend", intensity);

        // shader uniforms
        Vector2 sensitivity = new Vector2(sensitivityDepth, sensitivityNormals);
        material.SetVector("_Sensitivity", new Vector4(sensitivity.x, sensitivity.y, 1.0f, sensitivity.y));
        material.SetFloat("_BgFade", edgesOnly);
        material.SetFloat("_SampleDistance", sampleDist);
        material.SetVector("_BgColor", edgesOnlyBgColor);

        Graphics.Blit(source, destination, material,0);
    }

}
