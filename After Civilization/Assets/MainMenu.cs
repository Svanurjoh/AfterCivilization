﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Start() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void PlayGame()
	{
		SceneManager.LoadScene("Main");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
