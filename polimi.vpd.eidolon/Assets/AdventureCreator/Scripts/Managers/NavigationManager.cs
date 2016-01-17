/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2016
 *	
 *	"NavigationManager.cs"
 * 
 *	This script instantiates the chosen
 *	NavigationEngine subclass at runtime.
 * 
 */

using UnityEngine;

namespace AC
{

	/**
	 * This component instantiates the scene's chosen NavigationEngine ScriptableObject when the game begins.
	 * It should be placed on the GameEngine prefab.
	 */
	public class NavigationManager : MonoBehaviour
	{

		/** The NavigationEngine ScriptableObject that performs the scene's pathfinding algorithms. */
		[HideInInspector] public NavigationEngine navigationEngine = null;
		

		/**
		 * Initialises the Navigation Engine.  This is public so we have control over when it is called in relation to other Awake functions.
		 */
		public void OnAwake ()
		{
			navigationEngine = null;
			ResetEngine ();
		}


		/**
		 * Sets up the scene's chosen NavigationEngine ScriptableObject if it is not already present.
		 */
		public void ResetEngine ()
		{
			string className = "";
			if (KickStarter.sceneSettings.navigationMethod == AC_NavigationMethod.Custom)
			{
				className = KickStarter.sceneSettings.customNavigationClass;
			}
			else
			{
				className = "NavigationEngine_" + KickStarter.sceneSettings.navigationMethod.ToString ();
			}

			if (className == "" && Application.isPlaying)
			{
				ACDebug.LogWarning ("Could not initialise navigation - a custom script must be assigned if the Pathfinding method is set to Custom.");
			}
			else if (navigationEngine == null || !navigationEngine.ToString ().Contains (className))
			{
				navigationEngine = (NavigationEngine) ScriptableObject.CreateInstance (className);
				if (navigationEngine != null)
				{
					navigationEngine.OnReset (KickStarter.sceneSettings.navMesh);
				}
			}
		}

	}

}