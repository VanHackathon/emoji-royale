using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeLevel : MonoBehaviour {

	private string mLevel;

	void Start(){
		Advertisement.Initialize ("1378778", true);
	}
	public void changeLevel(string level)
	{
		StartCoroutine (ShowAdWhenReady());
		mLevel = level;
		//SceneManager.LoadScene(level);
	}

	IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady("video"))
            yield return null;

        var options = new ShowOptions { resultCallback = HandleShowResult };
      	Advertisement.Show("video", options);
    }

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");
				//
				// YOUR CODE TO REWARD THE GAMER
				// Give coins etc.
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
				break;
		}

		SceneManager.LoadScene(mLevel);
	}


}
