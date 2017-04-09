using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterButton : MonoBehaviour {

	public int monsterIndex;
	public int timer = 2;
	//public Monster monsterRef;
	//public Button yourButton;

	//public GameObject mobaManager;

	void Start()
	{
        InvokeRepeating("counter", 0, 0.2f);
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(CreateMonster);
		Text[] text = GetComponentsInChildren<Text>();
		text[0].text = MobaManager.instance.GetComponent<MobaManager> ().monsters [monsterIndex].GetComponent<Monster> ().powerCost.ToString();
	}

    public void counter()
    {
        timer++;
    }

    void CreateMonster()
	{
        if (timer > 1)
        {
            timer = 0;
            Debug.Log ("clicked" + monsterIndex);
		    MobaManager.instance.GetComponent<MobaManager> ().spawnPlayerMonster (monsterIndex);
        }
    }
}
