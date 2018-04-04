using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	private Transform originalPosition;

	void Awake() {
		originalPosition = transform;
	}

	void Update() {
		
	}

	public void returnGem() {
		transform.position = originalPosition.position;
	}

}
