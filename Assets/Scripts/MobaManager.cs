using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaManager : MonoBehaviour {

	public GameObject spawnPlayer;
	public GameObject spawnAI;
	private int power;
	private int playerHealth;
	private int enemyHealth;

	// Use this for initialization
	void Start () {
		power = 0;
		playerHealth = 100;
		enemyHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
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
