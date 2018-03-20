using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public GameObject[] gemSpawns;
	public GameObject[] enemySpawns;
	public GameObject[] enemyMassSpawns;
	public GameObject[] shipGemSlots;
	public GameObject redGemPrefab;
	public GameObject enemyPrefab;
	public PlayerController player;

	private int gemsDelivered = 0;
	private int maxGems = 6;

	public static GameManagerScript instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}

		for (int i = 0; i < gemSpawns.Length; i++) {
			Instantiate (redGemPrefab, gemSpawns [i].transform.position, redGemPrefab.transform.rotation);
		}
		for (int i = 0; i < 5; i++) {
			Instantiate (enemyPrefab, enemyMassSpawns [0].transform.position, enemyPrefab.transform.rotation);
		}
		for (int i = 0; i < 10; i++) {
			Instantiate (enemyPrefab, enemyMassSpawns [1].transform.position, enemyPrefab.transform.rotation);
		}
		for (int i = 0; i < enemySpawns.Length; i++) {
			Instantiate (enemyPrefab, enemySpawns [i].transform.position, enemyPrefab.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gemsDelivered == maxGems) {
			SceneManager.LoadScene ("EndMenu");
		}

		if (null == player) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}

	public int getGemsDelivered() {
		return gemsDelivered;
	}

	public void deliverGem(GameObject gem) {
		gem.transform.position = shipGemSlots [gemsDelivered].transform.position;
		gemsDelivered++;
	}
}
