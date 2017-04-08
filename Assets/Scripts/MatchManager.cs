using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MatchManager : MonoBehaviour {

	Smiley smiley1 = null;
	Smiley smiley2 = null;
	public GameObject[] smileyPrefabs;
	List<GameObject> smileyPool = new List<GameObject>();
	static int rows = 6;
	static int columns = 5;
	[SerializeField]
	float gap;

	Smiley[,] smileys = new Smiley[rows, columns];

	// Use this for initialization
	void Start () {
		GenerateSmileysPool();
		ShufflePool();

		FillMap();
	}
	
	// Update is called once per frame
	void Update () {
		
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
