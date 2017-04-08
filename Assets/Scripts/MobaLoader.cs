using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaLoader : MonoBehaviour {

	public GameObject mobaManager;

	void Awake()
	{
		if (MobaManager.instance == null) {
			Instantiate (mobaManager);
		}
	}

}
