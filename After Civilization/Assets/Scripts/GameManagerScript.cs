using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public GameObject[] gemSpawns;
	public GameObject[] shipGemSlots;
	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public GameObject level4;
	public GameObject level5;
	public GameObject level6;
	public GameObject redGemPrefab;
	public PlayerController player;

	public Image holdingGem;

	private int gemsDelivered = 0;
	private int maxGems = 6;
	private bool isPaused = false;

	#region level varibles

	//Gems
	private GameObject gem1;
	private GameObject gem2;
	private GameObject gem3;
	private GameObject gem4;
	private GameObject gem5;
	private GameObject gem6;

	//Levels
	private GameObject lvl1;
	private GameObject lvl2;
	private GameObject lvl3;
	private GameObject lvl4;
	private GameObject lvl5;
	private GameObject lvl6;

	#endregion

	public static GameManagerScript instance;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		lvl1 = Instantiate (level1, level1.transform.position, level1.transform.rotation);
		lvl2 = Instantiate (level2, level2.transform.position, level2.transform.rotation);
		lvl3 = Instantiate (level3, level3.transform.position, level3.transform.rotation);
		lvl4 = Instantiate (level4, level4.transform.position, level4.transform.rotation);
		lvl5 = Instantiate (level5, level5.transform.position, level5.transform.rotation);
		lvl6 = Instantiate (level6, level6.transform.position, level6.transform.rotation);

		holdingGem = GameObject.FindGameObjectWithTag ("GemHolder").GetComponent<Image> ();

		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError ("Two GameManager's Active, fix this ASAP!!");
			Destroy (this);
		}

		//Gems spawned
		gem1 = Instantiate (redGemPrefab, gemSpawns [0].transform.position, redGemPrefab.transform.rotation);
		gem2 = Instantiate (redGemPrefab, gemSpawns [1].transform.position, redGemPrefab.transform.rotation);
		gem3 = Instantiate (redGemPrefab, gemSpawns [2].transform.position, redGemPrefab.transform.rotation);
		gem4 = Instantiate (redGemPrefab, gemSpawns [3].transform.position, redGemPrefab.transform.rotation);
		gem5 = Instantiate (redGemPrefab, gemSpawns [4].transform.position, redGemPrefab.transform.rotation);
		gem6 = Instantiate (redGemPrefab, gemSpawns [5].transform.position, redGemPrefab.transform.rotation);
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
				closest = enemy;
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

		if (Input.GetKeyDown (KeyCode.P)) {
			isPaused = !isPaused;
		}
		Time.timeScale = isPaused ? 0 : 1;
	}

	public int getGemsDelivered() {
		return gemsDelivered;
	}

	public void deliverGem(GameObject gem) {
		gem.transform.position = shipGemSlots [gemsDelivered].transform.position;
		gemsDelivered++;
	}

	public void resetAllLevels ()
	{
		Destroy (lvl1);
		Destroy (lvl2);
		Destroy (lvl3);
		Destroy (lvl4);
		Destroy (lvl5);
		Destroy (lvl6);

		lvl1 = Instantiate (level1, level1.transform.position, level1.transform.rotation);
		lvl2 = Instantiate (level2, level2.transform.position, level2.transform.rotation);
		lvl3 = Instantiate (level3, level3.transform.position, level3.transform.rotation);
		lvl4 = Instantiate (level4, level4.transform.position, level4.transform.rotation);
		lvl5 = Instantiate (level5, level5.transform.position, level5.transform.rotation);
		lvl6 = Instantiate (level6, level6.transform.position, level6.transform.rotation);
	}

	public bool getIsPaused() {
		return isPaused;
	}
}
