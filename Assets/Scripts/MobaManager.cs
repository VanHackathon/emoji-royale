using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus {
	play,gameover,win
}

public class MobaManager : MonoBehaviour {

	public static MobaManager instance = null;
	public GameObject spawnPlayer;
	public GameObject spawnAI;
	public GameObject[] monsters;
	private int power;
	private int playerHealth;
	private int enemyHealth;
	private Text powerLbl;
	private gameStatus currentState = gameStatus.play;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		power = 0;
		playerHealth = 100;
		enemyHealth = 100;
		//spawnPlayerMonster (1);
		//spawnEnemyMonster (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnPlayerMonster(int index)
	{
		GameObject monster = Instantiate (monsters [index]) as GameObject;
		monster.transform.position = spawnPlayer.transform.position;

		int offset = Random.Range(-2, 2);
		Vector3 pos = new Vector3(monster.transform.position.x + offset, monster.transform.position.y, monster.transform.position.z);
		monster.transform.position = pos;
	}

	public void spawnEnemyMonster(int index)
	{
		GameObject enemyMonster = Instantiate (monsters [index]) as GameObject;
		enemyMonster.transform.position = spawnAI.transform.position;

		int offset = Random.Range(-2, 2);
		Vector3 pos = new Vector3(enemyMonster.transform.position.x + offset, enemyMonster.transform.position.y, enemyMonster.transform.position.z);
		enemyMonster.transform.position = pos;
	}

	public void addPower(int amount)
	{
		power = power + amount;
	}

	public void usePower(int amount)
	{
		power = power - amount;
	}

	public void doDamageToPlayer(int damage)
	{
		playerHealth = playerHealth - damage;
	}

	public void doDamageToEnemy(int damage)
	{
		enemyHealth = enemyHealth - damage;
	}

}
