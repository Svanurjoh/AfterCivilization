using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;
	public int score;
	// Use this for initialization

	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Debug.LogError ("Two GameManager's Active, fix this ASAP!!");
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		var tmp = GameObject.FindGameObjectWithTag ("EndGameScore");

		if (null != tmp) {
			tmp.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
		}
	}
}
