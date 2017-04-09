using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaLoader : MonoBehaviour {

	public GameObject mobaManager;

    //[SerializeField] private AudioClip explosionMoba;

    void Awake()
	{
		if (MobaManager.instance == null) {
            Instantiate (mobaManager);
            //MobaManager.instance.explosionMoba = explosionMoba;
        }
	}

}
