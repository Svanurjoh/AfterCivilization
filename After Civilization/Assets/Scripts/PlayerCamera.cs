using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
	public Transform target;
	public GameObject other;
	float distance = 10.0f;
	float xSpeed = 250.0f;
	float ySpeed = 120.0f;

	int yMinLimit = -20;
	int yMaxLimit = 80; 

	int zoomRate = 20;

	private float x = 0.0f;
	private float y = 0.0f;
	private Rigidbody rb;

	void Start()
	{
		x = transform.eulerAngles.x;
		y = transform.eulerAngles.y;

		rb = other.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
	}

	void LateUpdate()
	{
		if (Input.GetMouseButton (0)) {
			x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
			float test = 0;
			test = y;
		}
		distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
		if (distance < 2.5f) {
			distance = 2.5f;
		}
		if (distance > 20f) {
			distance = 20f;
		}

		y = ClampAngle(y, yMinLimit, yMaxLimit);

		Quaternion rotation;
		Vector3 position;

		transform.rotation = Quaternion.Euler (y, x, 0);
		transform.position = Quaternion.Euler (y, x, 0) * new Vector3 (0.0f, 2.0f, -distance) + target.position;
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle,min,max);
	}
}
