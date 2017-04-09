using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecs : MonoBehaviour {

	public float delay = 2.0f; //This implies a delay of 2 seconds.

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, delay);
	}
}
