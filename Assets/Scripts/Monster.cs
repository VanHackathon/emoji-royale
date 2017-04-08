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
}
