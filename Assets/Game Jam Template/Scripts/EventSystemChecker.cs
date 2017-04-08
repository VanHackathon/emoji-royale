using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventSystemChecker : MonoBehaviour
{
    // Any static function tagged with RuntimeInitializeOnLoadMethod will be called only a single time when the game load
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static public void InitSceneCallback()
    {
        SceneManager.sceneLoaded += SceneWasLoaded;
    }

    // This used to be OnLevelWasLoaded (now deprecated). It is registered as a callback on Scene Load in the
    // SceneManager in the function above.
    static public void SceneWasLoaded(Scene scene, LoadSceneMode mode)
	{
		//If there is no EventSystem (needed for UI interactivity) present
		if(!FindObjectOfType<EventSystem>())
		{
			//The following code instantiates a new object called EventSystem
			GameObject obj = new GameObject("EventSystem");

			//And adds the required components
			obj.AddComponent<EventSystem>();
			obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
		}
	}
}
