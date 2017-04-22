using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour{
    private int timelimit;
    private int multiplier = 1;
    private int counter = 0;
    public MobaManager moba;

    // Use this for initialization
    void Start()
    {
        timelimit = Random.Range(2, 6);
        moba = MobaManager.instance;//FindObjectOfType(typeof(MobaManager)) as MobaManager;
        InvokeRepeating("enemyAI", 0, 1);
        InvokeRepeating("enemyCash", 0, 1);
    }

    public void enemyAI()
    {
        int rand = Random.Range(3, 6);
        moba.spawnEnemyMonster(rand);
    }

    public void enemyCash()
    {
        //if (counter > timelimit)
        if (counter > Random.Range(2, 6))
        {
            counter = 0;
            multiplier++;
        }
        moba.enemyPower = moba.enemyPower + (multiplier * Random.Range(1, 2));
        counter++;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
