using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public GameObject[] gemSpawns;
	public GameObject[] enemySpawns;
	public GameObject redGemPrefab;
	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < gemSpawns.Length; i++) {
			Instantiate (redGemPrefab, gemSpawns [i].transform.position, redGemPrefab.transform.rotation);
		}
		for (int i = 0; i < 20; i++) {
			Instantiate (enemyPrefab, enemySpawns [0].transform.position, enemyPrefab.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
