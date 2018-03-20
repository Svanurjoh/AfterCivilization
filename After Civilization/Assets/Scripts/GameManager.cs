using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public PlayerController player;

	void Awake() {
		if (null == instance) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Debug.LogError ("Two GameManager's Active, fix this ASAP!!");
			Destroy (gameObject);
		}
	}

	void Start() {
		player = FindObjectOfType<PlayerController> ();
	}

}
