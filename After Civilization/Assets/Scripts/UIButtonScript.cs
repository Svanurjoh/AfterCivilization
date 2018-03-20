using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScript : MonoBehaviour {

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
