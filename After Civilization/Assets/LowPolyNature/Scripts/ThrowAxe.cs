using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour {

	private bool isTouching = false;
	private PlayerController _playerController;
	private Vector3 forward;

	void Awake() {
		_playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		forward = _playerController.transform.forward;
	}

	void Update() {
		
		transform.position += forward * Time.deltaTime * 10;

		transform.Rotate (Vector3.up * Time.deltaTime * 100, Space.Self);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyMesh") {
			Destroy (other.transform.parent.gameObject);
			Destroy (this.gameObject);
		}
	}

	public bool touching()
	{
		return isTouching;
	}
}
