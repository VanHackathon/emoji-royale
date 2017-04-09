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
		CheckCollision();
	}
	
	// Update is called once per frame
	void Update () {
		if (smiley1 && Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			GameObject collidedGO = hit.collider.gameObject;
			if (hit && collidedGO && collidedGO.gameObject.activeSelf && collidedGO != smiley1){
				smiley2 = collidedGO;
				smiley1.transform.localScale -= new Vector3(expandSmiley, expandSmiley, 0);
				Vector2 tempPos = smiley1.transform.position;
				// string tempName = smiley1.name;
				smiley1.transform.position = smiley2.transform.position;
				// smiley1.name = smiley2.name;
				smiley2.transform.position = tempPos;
				// smiley2.name = tempName;

				//finding which smileys[r, c] has smiley1 (and then smiley2) to change it in the matrix
				//it's a terrible code, I know, but it was 2a.m. and I was really sleepy
				int r1 = 0, c1 = 0;
				for (int r = 0; r < rows; r++)
					for (int c = 0; c < columns; c++)
						if (smileys[r, c].gameObject == smiley1){
							r1 = r;
							c1 = c;
						}

				for (int r = 0; r < rows; r++)
					for (int c = 0; c < columns; c++)
						if (smileys[r, c].gameObject == smiley2){
							Smiley s = smileys[r1, c1];
							smileys[r1, c1] = smileys[r, c];
							smileys[r, c] = s;
						}
				

				smiley1 = null;
				smiley2 = null;

				CheckCollision();
			}
			else if (hit && hit.collider.gameObject.activeSelf){	//same smiley
				smiley1.transform.localScale -= new Vector3(expandSmiley, expandSmiley, 0);
				smiley1 = null;
			}
		}
		else if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			if (hit && hit.collider.gameObject.activeSelf){
				smiley1 = hit.collider.gameObject;
				smiley1.transform.localScale += new Vector3(expandSmiley, expandSmiley, 0);
			}
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

	void CheckCollision(){
		//checking rows
		for (int r = 0; r < rows; r++){
			int counter = 1;
			for (int c = 1; c < columns; c++){
				Smiley s1 = smileys[r, c], s2 = smileys[r, c-1]; 
				if (s1 != null && s2 != null && s1.gameObject.activeSelf && s2.gameObject.activeSelf && s1.name == s2.name)
					counter++;
				else
					counter = 1;

				if (counter >= 3){
					s1.gameObject.SetActive(false);
					s2.gameObject.SetActive(false);
					smileys[r, c-2].gameObject.SetActive(false);
				}
			}
		}

		//checking columns
		for (int c = 0; c < columns; c++){
			int counter = 1;
			for (int r = 1; r < rows; r++){
				Smiley s1 = smileys[r, c], s2 = smileys[r-1, c]; 
				if (s1 != null && s2 != null && s1.name == s2.name)
					counter++;
				else
					counter = 1;

				if (counter >= 3){
					s1.gameObject.SetActive(false);
					s2.gameObject.SetActive(false);
					smileys[r-2, c].gameObject.SetActive(false);
				}
			}
		}
	}
}
