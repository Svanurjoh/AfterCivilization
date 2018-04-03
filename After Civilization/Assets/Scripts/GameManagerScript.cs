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
			Debug.LogError ("Two GameManager's Active, fix this ASAP!!");
			Destroy (this);
		}

		for (int i = 0; i < gemSpawns.Length; i++) {
			Instantiate (redGemPrefab, gemSpawns [i].transform.position, redGemPrefab.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gemsDelivered == maxGems) {
			SceneManager.LoadScene ("EndMenu");
		}

		if (null == player) {
			var tmp = GameObject.FindGameObjectWithTag ("Player");
			if (null != tmp) {
				player = tmp.GetComponent<PlayerController> ();
			}
		}

		var enemies = FindObjectsOfType<EnemyChase> ();
		float leastDist = 100f;
		EnemyChase closest = null;
		for (var i = 0; i < enemies.Length; i++) {
			var enemy = enemies [i];
			enemy.canShout = false;
			float distToPLayer = Vector3.Distance (enemy.transform.position, player.transform.position);
			if (i == 0) {
				leastDist = distToPLayer;
			} else {
				if (distToPLayer <= leastDist) {
					leastDist = distToPLayer;
					closest = enemy;
				}
			}
		}
		if (null != closest) {
			closest.canShout = true;
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
