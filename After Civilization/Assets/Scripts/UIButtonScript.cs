using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonScript : MonoBehaviour {

	void Awake() {
	}

	public void NewGameButton()
	{
		SceneManager.LoadScene ("Main");
	}

	public void ControlsButton(string controls)
	{}

	public void ExitButton()
	{
		Application.Quit ();
	}


}
