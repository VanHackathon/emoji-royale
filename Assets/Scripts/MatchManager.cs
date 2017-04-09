using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MatchManager : MonoBehaviour
{

    GameObject smiley1 = null;
    GameObject smiley2 = null;
    public GameObject[] smileyPrefabs;
    List<GameObject> smileyPool = new List<GameObject>();
    static int rows = 5;
    static int columns = 5;
    static bool gameRunning = false;
    [SerializeField]
    float gap;
    private float expandSmiley = 0.2f;
    [SerializeField]
    private AudioClip sfxMatch;
    private AudioSource audioSource;

    Smiley[,] smileys = new Smiley[rows, columns];

	private MobaManager mm;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameRunning = false;
        mm = MobaManager.instance;
        GenerateSmileysPool();
        ShufflePool();
        FillMap();
        CheckCollision();
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (smiley1 && Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Debug.DrawRay (ray.origin, ray.direction, Color.green);
            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			Debug.Log ("Worldpoint:" + worldPoint);
            if (hit)
            {
                if (hit.collider == null)
                    return;
                GameObject collidedGO = hit.collider.gameObject;
                if (hit && collidedGO && collidedGO != smiley1 && collidedGO.tag == "Smiley")
                {
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
                            if (smileys[r, c] != null && smileys[r, c].gameObject == smiley1)
                            {
                                r1 = r;
                                c1 = c;
                            }

                    for (int r = 0; r < rows; r++)
                        for (int c = 0; c < columns; c++)
                            if (smileys[r, c] != null && smileys[r, c].gameObject == smiley2)
                            {
                                Smiley s = smileys[r1, c1];
                                smileys[r1, c1] = smileys[r, c];
                                smileys[r, c] = s;
                            }


                    smiley1 = null;
                    smiley2 = null;

                    CheckCollision();
                }
                else if (hit && hit.collider.gameObject)
                {   //same smiley
                    smiley1.transform.localScale -= new Vector3(expandSmiley, expandSmiley, 0);
                    smiley1 = null;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Debug.DrawRay (ray.origin, ray.direction, Color.green);
			//RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			Debug.Log ("Worldpoint:" + worldPoint);
            if (hit)
            {
                GameObject collidedGO = hit.collider.gameObject;
                if (collidedGO && collidedGO.tag == "Smiley")
                {
                    smiley1 = hit.collider.gameObject;
                    smiley1.transform.localScale += new Vector3(expandSmiley, expandSmiley, 0);
                }
            }
        }
    }

    // Fills map with jewels
    void FillMap()
    {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                if (smileys[i, j] == null)
                {
                    Vector2 smileyPos = new Vector2(1 + i + gap + i * gap, -3 + j + gap + j * gap);
                    for (int x = 0; x < smileyPool.Count; x++)
                    {
                        GameObject o = smileyPool[x];
                        if (!o.activeSelf)
                        {
                            o.transform.position = smileyPos;
                            o.SetActive(true);
                            smileys[i, j] = new Smiley(o, o.name);
                            break;
                        }
                    }
                }
    }

    void GenerateSmileysPool()
    {
        int numCopies = rows * columns / 2;
        for (int i = 0; i < numCopies; i++)
        {
            for (int j = 0; j < smileyPrefabs.Length; j++)
            {
                GameObject o = (GameObject)Instantiate(smileyPrefabs[j], new Vector2(-10, -10), smileyPrefabs[j].transform.rotation);
                o.SetActive(false);
                smileyPool.Add(o);
            }
        }
        //ShufflePool();
    }

    void ShufflePool()
    {
        System.Random rand = new System.Random();
        int r = smileyPool.Count;
        while (r > 1)
        {
            r--;
            int n = rand.Next(r + 1);
            GameObject val = smileyPool[n];
            smileyPool[n] = smileyPool[r];
            smileyPool[r] = val;
        }
    }

    void CheckCollision()
    {
        bool hasCollision = false;
        //checking rows
        for (int r = 0; r < rows; r++)
        {
            int counter = 1;
            for (int c = 1; c < columns; c++)
            {
                Smiley s1 = smileys[r, c], s2 = smileys[r, c - 1];
                if (s1 != null && s2 != null && s1.name == s2.name)
                    counter++;
                else
                    counter = 1;

                if (counter >= 3)
                {
                    hasCollision = true;
                    s1.gameObject.SetActive(false);
                    s2.gameObject.SetActive(false);
                    smileys[r, c - 2].gameObject.SetActive(false);
					UpdatePower (counter);
                }
            }

        }

        //checking columns
        for (int c = 0; c < columns; c++)
        {
            int counter = 1;
            for (int r = 1; r < rows; r++)
            {
                Smiley s1 = smileys[r, c], s2 = smileys[r - 1, c];
                if (s1 != null && s2 != null && s1.name == s2.name)
                    counter++;
                else
                    counter = 1;

                if (counter >= 3)
                {
                    hasCollision = true;
                    s1.gameObject.SetActive(false);
                    s2.gameObject.SetActive(false);
                    smileys[r - 2, c].gameObject.SetActive(false);
					UpdatePower (counter);

                }
            }


        }

        //moving all non active smileys to the puta que pariu
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                if (smileys[r, c] != null && !smileys[r, c].gameObject.activeSelf)
                {
                    smileys[r, c].gameObject.transform.position = new Vector3(-10, -10, 0);
                    smileys[r, c] = null;
                }

		if (hasCollision)
            Invoke("MoveSmileys", 0.5f);
    }

	//counting twice when hiting 4 blocks, but still make a good score
	void UpdatePower (int counter)
	{
        audioSource.PlayOneShot(sfxMatch);
        //audioSource.Play();
        if (gameRunning){
            Debug.Log ("Counter:" + counter);
            mm.addPower (20 * counter);
        }
	}

	void MoveSmileys(){
		bool anyMoved = false;
		ShufflePool();

		// for (int r = 0; r < rows; r++)
		// 	for (int c = 0 ; c < columns; c++)
		// 		if (smileys[c, r] == null){
		// 			Vector2 smileyPos = new Vector2(r + gap + r * gap, c + gap + c * gap);
		// 			for (int n = 0; n < smileyPool.Count; n++){
		// 				GameObject o = smileyPool[n];
		// 				if (!o.activeSelf){
		// 					o.transform.position = smileyPos;
		// 					o.SetActive(true);
		// 					smileys[r, c] = new Smiley(o, o.name);
		// 				}
		// 			}
		// 		}

		// FillMap();

		for (int r = 1; r < rows; r++)
			for (int c = 0; c < columns; c++){
				if (smileys[c, r] != null && smileys[c, r-1] == null){
					smileys[c, r-1] = smileys[c, r];
					smileys[c, r-1].gameObject.transform.position = new Vector3(1 + c + gap + c * gap, -3 + r-1 + gap + (r-1) * gap, 0);
					smileys[c, r] = null;
					anyMoved = true;
				}
				if (r == rows - 1 && smileys[c, r] == null){
					anyMoved = true;
					Vector2 smileyPos = new Vector2(1 + c + gap + c * gap, -3 + r + gap + r * gap);
					for (int n = 0; n < smileyPool.Count; n++){
						GameObject o = smileyPool[n];
						if (!o.activeSelf){
							o.transform.position = smileyPos;
							o.SetActive(true);
							smileys[c, r] = new Smiley(o, o.name);
							break;
						}
					}
				}
			}
		
		if (anyMoved)
			Invoke("MoveSmileys", 0.5f);
		else
			CheckCollision();
	}
}
