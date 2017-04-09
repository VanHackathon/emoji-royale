using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus {
	play,gameover,win
}

public class MobaManager : MonoBehaviour {

	public const string TAG_ENEMY = "Enemy";
	public const string TAG_PLAYER = "Player";

	public static MobaManager instance = null;
	public GameObject spawnPlayer;
	public GameObject spawnAI;
	public GameObject[] monsters;
	private int power = 500;
	private int playerHealth = 1000;
	private int enemyHealth = 400;
	public Text powerLbl;
	public Text enemyHpLbl;
	public Text playerHpLbl;

	private gameStatus currentState = gameStatus.play;


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		powerLbl.text = power.ToString ();
		enemyHpLbl.text = enemyHealth.ToString ();
		playerHpLbl.text = playerHealth.ToString ();
	}
	// Use this for initialization
	void Start () {
        InvokeRepeating("enemyAI", 0, 1);
    }

    public void enemyAI()
    {
        int rand = Random.Range(0, 3);
        spawnEnemyMonster(rand);
    }

    // Update is called once per frame
    void Update () {
		
	}

	public void spawnPlayerMonster(int index)
	{
		GameObject monster = Instantiate (monsters [index]) as GameObject;
		int cost = monster.GetComponent<Monster> ().powerCost;
		if (cost <= power) {
			usePower (cost);
			monster.transform.position = spawnPlayer.transform.position;
			monster.tag = TAG_PLAYER;

			int offset = Random.Range (-2, 2);
			Vector3 pos = new Vector3 (monster.transform.position.x + offset, monster.transform.position.y, monster.transform.position.z);
			monster.transform.position = pos;
		} else {
			Destroy (monster);
		}
	}

	public void spawnEnemyMonster(int index)
	{
		GameObject enemyMonster = Instantiate (monsters [index]) as GameObject;
		enemyMonster.transform.position = spawnAI.transform.position;
		enemyMonster.tag = TAG_ENEMY;

        Monster mon = enemyMonster.GetComponent<Monster>();
        mon.speed = mon.speed * (-1); // make enemy monster go down

        int offset = Random.Range(-2, 2);
		Vector3 pos = new Vector3(enemyMonster.transform.position.x + offset, enemyMonster.transform.position.y, enemyMonster.transform.position.z);
		enemyMonster.transform.position = pos;
	}

	public void addPower(int amount)
	{
		power = power + amount;
		powerLbl.text = power.ToString ();
	}

	public void usePower(int amount)
	{
		power = power - amount;
		powerLbl.text = power.ToString ();
	}

	public void doDamageToPlayer(int damage)
	{
		playerHealth = playerHealth - damage;
		playerHpLbl.text = playerHealth.ToString ();
		Debug.Log ("playerHealth:" + playerHealth);
		if (playerHealth <= 0) {
			Debug.Log ("Game Over - You Lost");
			currentState = gameStatus.gameover;
		}
	}

	public void doDamageToEnemy(int damage)
	{
		enemyHealth = enemyHealth - damage;
		enemyHpLbl.text = enemyHealth.ToString ();
		Debug.Log ("enemyHealth:" + enemyHealth);
		if (enemyHealth <= 0) {
			Debug.Log ("Game Over - You WIN");
			currentState = gameStatus.gameover;
		}
	}

}
