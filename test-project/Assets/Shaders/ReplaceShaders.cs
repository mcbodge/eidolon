using UnityEngine;
using System.Collections;
using System.Linq;

public class ReplaceShaders : MonoBehaviour {

	public static Shader myShader;

	void Start () {

		myShader = Shader.Find( "Normals" );

	 	GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
	 	
		foreach(GameObject myObj in allObjects) {		
		  
			if (myObj.GetComponent<Renderer>() && myObj.tag!="Color") {		 
				Material[] allMaterials = myObj.GetComponent<Renderer>().materials;				
				foreach(Material mat in allMaterials)				
					mat.shader = myShader;					
		  }
			
	  }
		
  }
	
	void Update () {
	
	}
}
