using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneConfig : MonoBehaviour {

	private GameStats gs;
	//public Text resultLbl;
	public Text monstersKilledLbl;
	public Text timeLbl;
	public Text totalPowerLbl;
	public GameObject win;
	public GameObject lose;

	// Use this for initialization
	void Start () {
		gs = GameStats.instance;
		if (gs.result == gameStatus.win) {
			//resultLbl.text = "YOU WIN!";
			win.SetActive(true);
		} else {
			//resultLbl.text = "YOU LOSE!";
			lose.SetActive(true);
		}

		monstersKilledLbl.text = "Monsters Killed: "+gs.monstersKilled.ToString ();

		int minutes = gs.timePlayedInSec/60;
		int secs = gs.timePlayedInSec%60;
		if (secs < 10) {
			timeLbl.text = "Time Played: " + minutes + ":0" + secs;
		} else {
			timeLbl.text = "Time Played: " + minutes + ":" + secs;
		}

		totalPowerLbl.text = "Total Power: " + gs.totalPower.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
