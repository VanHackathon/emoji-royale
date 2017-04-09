using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int health = 100;
	public int attack = 20;
	public int speed = 10;
	public int powerCost = 20;

	public ParticleSystem ps;

    [SerializeField]
    private AudioClip sfxExplosion;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("collider: " + other.ToString ());
		if (gameObject.tag == MobaManager.TAG_PLAYER) {
            if (other.name == "EnemyCastle") {
                Debug.Log ("Castle Dmg: " + attack);
				MobaManager.instance.doDamageToEnemy (attack);
				ParticleSystem localPS = Instantiate (ps);
				localPS.transform.position = gameObject.transform.position;

				Destroy(gameObject);
            }
			if (other.tag == MobaManager.TAG_ENEMY) {
                MobaManager.instance.addMonsterKilled ();
				ParticleSystem localPS = Instantiate (ps);
				localPS.transform.position = gameObject.transform.position;

				Destroy(other.gameObject);
				Destroy(gameObject);
            }
            audioSource.PlayOneShot(sfxExplosion);

        }
		else if (gameObject.tag == MobaManager.TAG_ENEMY) {
            if (other.name == "PlayerCastle") {
                Debug.Log ("Player Castle Dmg: " + attack);
				Debug.Log ("monster position =" + gameObject.transform.position);
				MobaManager.instance.doDamageToPlayer (attack);
				ParticleSystem localPS = Instantiate (ps);
				localPS.transform.position = gameObject.transform.position;
				Debug.Log ("particle position =" + localPS.transform.position);

				Destroy(gameObject);
			}
			if (other.tag == MobaManager.TAG_PLAYER) {
                ParticleSystem localPS = Instantiate (ps);
				localPS.transform.position = gameObject.transform.position;

				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		}
	}
}
