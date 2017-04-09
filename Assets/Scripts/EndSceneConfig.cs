using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneConfig : MonoBehaviour {

	private GameStats gs;
	public Text result;

	// Use this for initialization
	void Start () {
		gs = GameStats.instance;
		if (gs.result == gameStatus.win) {
			result.text = "YOU WIN!";
		} else {
			result.text = "YOU LOSE!";
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
