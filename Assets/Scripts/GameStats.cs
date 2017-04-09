using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour {

	public static GameStats instance = null;

	public gameStatus result = gameStatus.win;
	public int monstersKilled = 0;
	public int timePlayedInSec = 0;
	public int totalPower = 0;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

}
