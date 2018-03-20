using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public GameObject[] gemSpawns;
	public GameObject[] enemySpawns;
	public GameObject[] enemyMassSpawns;
	public GameObject[] shipGemSlots;
	public GameObject redGemPrefab;
	public GameObject enemyPrefab;
	public PlayerController player;

	private int gemsDelivered = 0;
	public static GameManagerScript instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();

		for (int i = 0; i < gemSpawns.Length; i++) {
			Instantiate (redGemPrefab, gemSpawns [i].transform.position, redGemPrefab.transform.rotation);
		}
		for (int i = 0; i < 5; i++) {
			Instantiate (enemyPrefab, enemyMassSpawns [0].transform.position, enemyPrefab.transform.rotation);
		}
		for (int i = 0; i < 5; i++) {
			Instantiate (enemyPrefab, enemyMassSpawns [1].transform.position, enemyPrefab.transform.rotation);
		}
		for (int i = 0; i < enemySpawns.Length; i++) {
			Instantiate (enemyPrefab, enemySpawns [i].transform.position, enemyPrefab.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public int getGemsDelivered() {
		return gemsDelivered;
	}

	public void deliverGem(GameObject gem) {
		gem.transform.position = shipGemSlots [gemsDelivered].transform.position;
		gemsDelivered++;
	}
}
