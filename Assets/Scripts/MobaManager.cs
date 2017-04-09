using System.Collections;
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
	public int enemyPower = 350;
	private int playerHealth = 100;
	private int enemyHealth = 100;
	private GameObject powerLbl;
	private GameObject enemyHpLbl;
	private GameObject playerHpLbl;

	public gameStatus currentState = gameStatus.play;
	public int monstersKilled = 0;

	public float timer = 0;
	public int totalPower = 0;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		//DontDestroyOnLoad(gameObject);
		powerLbl = GameObject.Find("PowerValue");
		enemyHpLbl = GameObject.Find("EnemyHPValue");
		playerHpLbl = GameObject.Find("PlayerHPValue");

		powerLbl.GetComponent<Text>().text = power.ToString ();
		enemyHpLbl.GetComponent<Text>().text = enemyHealth.ToString ();
		playerHpLbl.GetComponent<Text>().text = playerHealth.ToString ();
	}
	// Use this for initialization
	void Start () {
		currentState = gameStatus.play;
    }
    
    // Update is called once per frame
    void Update () {
		timer += Time.deltaTime;
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
        GameObject enemyMonster = Instantiate(monsters[index]) as GameObject;
        Monster mon = enemyMonster.GetComponent<Monster>();
        int cost = mon.powerCost;
        if (cost <= enemyPower) {
            enemyPower = enemyPower - cost;
            enemyMonster.transform.position = spawnAI.transform.position;
            enemyMonster.tag = TAG_ENEMY;
            mon.speed = mon.speed * (-1); // make enemy monster go down

            int offset = Random.Range(-2, 2);
            Vector3 pos = new Vector3(enemyMonster.transform.position.x + offset, enemyMonster.transform.position.y, enemyMonster.transform.position.z);
            enemyMonster.transform.position = pos;
        }
        else {
            Destroy(enemyMonster);
        }

    }

	public void addPower(int amount)
	{
		totalPower = totalPower + amount;
		power = power + amount;
		powerLbl.GetComponent<Text>().text = power.ToString ();
	}

	public void usePower(int amount)
	{
		power = power - amount;
		powerLbl.GetComponent<Text>().text = power.ToString ();
	}

	public void doDamageToPlayer(int damage)
	{
		playerHealth = playerHealth - damage;
		playerHpLbl.GetComponent<Text>().text = playerHealth.ToString ();
		Debug.Log ("playerHealth:" + playerHealth);
		if (playerHealth <= 0) {
			Debug.Log ("Game Over - You Lost");
			currentState = gameStatus.gameover;
			updateGameStatus ();
			Application.LoadLevel ("EndGame");
		}
	}

	public void doDamageToEnemy(int damage)
	{
		enemyHealth = enemyHealth - damage;
		enemyHpLbl.GetComponent<Text>().text = enemyHealth.ToString ();
		Debug.Log ("enemyHealth:" + enemyHealth);
		if (enemyHealth <= 0) {
			Debug.Log ("Game Over - You WIN");
			currentState = gameStatus.win;
			updateGameStatus ();
			
			Application.LoadLevel ("EndGame");
		}
	}

	public void addMonsterKilled()
	{
		monstersKilled = monstersKilled + 1;
	}

	public void updateGameStatus()
	{
		GameStats.instance.result = currentState;
		GameStats.instance.monstersKilled = monstersKilled;
		Debug.Log ("Timer:" + (int) timer);
		GameStats.instance.timePlayedInSec = (int) timer;
		GameStats.instance.totalPower = totalPower;
	}

}
