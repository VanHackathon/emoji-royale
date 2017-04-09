using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ChangeLevel : MonoBehaviour {

	void Start(){
		//Advertisement.Initialize ("1378778", true);
	}
	public void changeLevel(string level)
	{
		///StartCoroutine (ShowAdWhenReady());
		Application.LoadLevel(level);
	}

	IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show ();
    }
}
