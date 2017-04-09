using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MobaManager{
    private int timelimit = 5;
    private int multiplier = 1;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("enemyAI", 0, 1);
        InvokeRepeating("enemyCash", 0, 1);
    }

    public void enemyAI()
    {
        int rand = Random.Range(0, 3);
        spawnEnemyMonster(rand);
    }

    public void enemyCash()
    {
        if (counter > timelimit)
        {
            counter = 0;
            multiplier++;
        }
        enemyPower = enemyPower + multiplier;
        counter++;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
