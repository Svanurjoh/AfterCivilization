using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonScript : MonoBehaviour {

	public Text endGameText;

	void Awake() {
		if (GameManagerScript.instance.getGemsDelivered () != 6) {
			endGameText.text = "LOOOOOOOOOOOOOOSER";
		}
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
