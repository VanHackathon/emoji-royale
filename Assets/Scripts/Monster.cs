using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int health = 100;
	public int attack = 20;
	public int speed = 10;
	public int powerCost = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("collider: " + other.ToString ());
		if (gameObject.tag == MobaManager.TAG_PLAYER) {
			if (other.name == "EnemyCastle") {
				Debug.Log ("Castle Dmg: " + attack);
				MobaManager.instance.doDamageToEnemy (attack);
				Destroy(gameObject);
			}
		}
		else if (gameObject.tag == MobaManager.TAG_ENEMY) {
			if (other.name == "PlayerCastle") {
				Debug.Log ("Player Castle Dmg: " + attack);
				MobaManager.instance.doDamageToPlayer (attack);
				Destroy(gameObject);
			}
		}
	}
}
