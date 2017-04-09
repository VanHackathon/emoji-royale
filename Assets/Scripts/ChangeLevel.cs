using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour {

	public void changeLevel(string level)
	{
		Application.LoadLevel(level);
	}
}
