using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public GameObject[] gemSpawns;
	public GameObject[] shipGemSlots;
	public GameObject[] rays;
	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public GameObject level4;
	public GameObject level5;
	public GameObject level6;
	public GameObject redGemPrefab;
	[HideInInspector]
	public PlayerController player;
	[HideInInspector]
	public Image holdingGem;

	private int gemsDelivered = 0;
	private int maxGems = 6;
	private bool isPaused = false;

	private int playerAxes;
	private int playerHealth;

	private float timer = 0f;
	private Text Timer;

	#region level varibles

	private bool gem1delivered = false;
	private bool gem2delivered = false;
	private bool gem3delivered = false;
	private bool gem4delivered = false;
	private bool gem5delivered = false;
	private bool gem6delivered = false;

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

		Timer = GameObject.FindGameObjectWithTag ("Timer").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		int min = (int)timer / 60;
		int sec = (int)timer % 60;
		string nil = "";
		if (sec < 10) {
			nil = "0";
		}
		Timer.text = min + ":" + nil + sec;

		if (gemsDelivered == maxGems) {
			SceneManager.LoadScene ("EndMenu");
		}

		if (null == player) {
			var tmp = GameObject.FindGameObjectWithTag ("Player");
			if (null != tmp) {
				player = tmp.GetComponent<PlayerController> ();
				playerHealth = player.Health;
				playerAxes = player.AxeCount;
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

		if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
		}
		if (isPaused) {
			Time.timeScale = 0;
			if(Input.GetKeyDown(KeyCode.Q))
				SceneManager.LoadScene ("MainMenu");
		}
		else 
			Time.timeScale = 1;
		
		Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
		Cursor.visible = isPaused ? true : false;
	}

	public void gemPickedUp(GameObject gem) {
		if (gem == gem1)
			rays [0].SetActive(false);
		if (gem == gem2)
			rays [1].SetActive(false);
		if (gem == gem3)
			rays [2].SetActive(false);
		if (gem == gem4)
			rays [3].SetActive(false);
		if (gem == gem5)
			rays [4].SetActive(false);
		if (gem == gem6)
			rays [5].SetActive(false);
	}

	public int getGemsDelivered() {
		return gemsDelivered;
	}

	public void deliverGem(GameObject gem) {
		gem.transform.position = shipGemSlots [gemsDelivered].transform.position;
		gemsDelivered++;
		if (gem == gem1)
			gem1delivered = true;
		if (gem == gem2)
			gem2delivered = true;
		if (gem == gem3)
			gem3delivered = true;
		if (gem == gem4)
			gem4delivered = true;
		if (gem == gem5)
			gem5delivered = true;
		if (gem == gem6)
			gem6delivered = true;

		playerHealth = player.Health;
		playerAxes = player.AxeCount;
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

		if (!gem1delivered) {
			gem1.transform.position = gemSpawns [0].transform.position;
			rays [0].SetActive (true);
		}
		if (!gem2delivered) {
			gem2.transform.position = gemSpawns [1].transform.position;
			rays [1].SetActive(true);
		}
		if (!gem3delivered) {
			gem3.transform.position = gemSpawns [2].transform.position;
			rays [2].SetActive (true);
		}
		if (!gem4delivered) {
			gem4.transform.position = gemSpawns [3].transform.position;
			rays [3].SetActive (true);
		}
		if (!gem5delivered) {
			gem5.transform.position = gemSpawns [4].transform.position;
			rays [4].SetActive (true);
		}
		if (!gem6delivered) {
			gem6.transform.position = gemSpawns [5].transform.position;
			rays [5].SetActive (true);
		}
	}

	public bool getIsPaused() {
		return isPaused;
	}

	public int GetPlayerAxes() {
		return playerAxes;
	}
	public int GetPlayerHealth() {
		return playerHealth;
	}
}
