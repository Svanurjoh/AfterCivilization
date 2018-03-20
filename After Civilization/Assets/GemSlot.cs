using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSlot : MonoBehaviour {

	public Sprite EquipedGem;
	public Sprite noGem;

	private Image img;
	private bool hasGem;

	void Start () {
		img = GetComponent<Image> ();
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.M)) {
			EquipGem();
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			DropGem();
		}
	}

	public void EquipGem () {
		hasGem = true;
		img.sprite = EquipedGem;
	}

	public void DropGem () {
		hasGem = false;
		img.sprite = noGem;
	}

	public bool HasGem() {
		return hasGem;
	}
}
