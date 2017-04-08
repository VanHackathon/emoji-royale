using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MatchManager : MonoBehaviour {

	GameObject smiley1 = null;
	GameObject smiley2 = null;
	public GameObject[] smileyPrefabs;
	List<GameObject> smileyPool = new List<GameObject>();
	static int rows = 6;
	static int columns = 5;
	[SerializeField]
	float gap;
	private float expandSmiley = 0.2f;

	Smiley[,] smileys = new Smiley[rows, columns];

	// Use this for initialization
	void Start () {
		GenerateSmileysPool();
		ShufflePool();

		FillMap();
	}
	
	// Update is called once per frame
	void Update () {
		if (smiley1 && Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			GameObject collidedGO = hit.collider.gameObject;
			if (hit && collidedGO != smiley1){
				smiley2 = collidedGO;
			}
			else if (hit){	//same smiley
				smiley1.transform.localScale -= new Vector3(expandSmiley, expandSmiley, 0);
				smiley1 = null;
			}
		}
		else if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			if (hit){
				smiley1 = hit.collider.gameObject;
				smiley1.transform.localScale += new Vector3(expandSmiley, expandSmiley, 0);
			}
		}
		if (smiley1 && smiley2){
			smiley1.transform.localScale -= new Vector3(expandSmiley, expandSmiley, 0);
			Vector2 tempPos = smiley1.transform.position;
			smiley1.transform.position = smiley2.transform.position;
			smiley2.transform.position = tempPos;
			smiley1 = null;
			smiley2 = null;
		}
	}

	// Fills map with jewels
	void FillMap() {
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++){
				Vector2 smileyPos = new Vector2(i + gap + i * gap, j + gap + j * gap);
				for (int x = 0; x < smileyPool.Count; x++){
					GameObject o = smileyPool[x];
					if (!o.activeSelf){
						o.transform.position = smileyPos;
						o.SetActive(true);
						smileys[i, j] = new Smiley(o, o.name);
						break;
					}
				}
			}
	}

	void GenerateSmileysPool(){
		int numCopies = rows * columns / 2;
		for (int i = 0; i < numCopies; i++){
			for (int j = 0; j < smileyPrefabs.Length; j++){
				GameObject o = (GameObject)Instantiate(smileyPrefabs[j], new Vector2(-10, -10), smileyPrefabs[j].transform.rotation);
				o.SetActive(false);
				smileyPool.Add(o);
			}
		}
		ShufflePool();
	}

	void ShufflePool(){
		System.Random rand = new System.Random();
		int r = smileyPool.Count;
		while (r > 1){
			r--;
			int n = rand.Next(r + 1);
			GameObject val = smileyPool[n];
			smileyPool[n] = smileyPool[r];
			smileyPool[r] = val;
		}
	}
}
