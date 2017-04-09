using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour{
    private int timelimit = 5;
    private int multiplier = 1;
    private int counter = 0;
    public MobaManager moba;

    // Use this for initialization
    void Start()
    {
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
        if (counter > timelimit)
        {
            counter = 0;
            multiplier++;
        }
        moba.enemyPower = moba.enemyPower + multiplier;
        counter++;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
