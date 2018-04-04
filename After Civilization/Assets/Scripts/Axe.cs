using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

	private bool isTouching = false;

	void Awake() {
		
	}

	void Update() {
	}

	void OnTriggerEnter(Collider other)
	{
		/*if (other.gameObject.tag == "EnemyMesh" && _playerController.getSwing()) {
			Debug.Log (_playerController.getSwing ());
			Destroy (other.transform.parent.gameObject);
		}*/
	}

	public bool touching()
	{
		return isTouching;
	}
}
