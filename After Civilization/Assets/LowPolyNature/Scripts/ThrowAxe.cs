using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour {

	private bool isTouching = false;
	private PlayerController _playerController;
	private Vector3 forward;
	private float alive;

	void Awake() {
		_playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		forward = _playerController.transform.forward;
		alive = 0f;
	}

	void Update() {
		if (alive <= 0.15f) {
			forward.y += Time.deltaTime;
		} else {
			forward.y -= Time.deltaTime;
		}
		alive += Time.deltaTime;

		transform.position += forward * Time.deltaTime * 25;

		transform.Rotate (Vector3.up * Time.deltaTime * 800, Space.Self);

		if (transform.position.y <= 0) {
			Destroy (this.gameObject);
		}
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
