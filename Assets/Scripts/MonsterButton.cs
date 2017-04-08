using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterButton : MonoBehaviour {

	public int monsterIndex;
	//public Monster monsterRef;
	//public Button yourButton;

	//public GameObject mobaManager;

	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(CreateMonster);
		Text[] text = GetComponentsInChildren<Text>();
		text[0].text = MobaManager.instance.GetComponent<MobaManager> ().monsters [monsterIndex].GetComponent<Monster> ().powerCost.ToString();
	}

	void CreateMonster()
	{
		Debug.Log ("clicked" + monsterIndex);
		MobaManager.instance.GetComponent<MobaManager> ().spawnPlayerMonster (monsterIndex);
	}
}
