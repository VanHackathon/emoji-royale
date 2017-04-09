using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintCreator : MonoBehaviour {
	public Text hint;
	// Use this for initialization
	void Start () {
		//createText("teste", Color.black, Vector2.zero);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Text textBoxRef;

	public void createText(string text, Color color, Vector2 pos)
	{
		if (textBoxRef != null)
			Destroy (textBoxRef);
		textBoxRef = Instantiate(hint,gameObject.transform);
		//textBox.transform.parent = gameObject.transform;
		textBoxRef.text = text;
		textBoxRef.color = color;
		textBoxRef.transform.position = pos;
	}
}
