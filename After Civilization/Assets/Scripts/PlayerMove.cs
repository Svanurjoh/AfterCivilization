using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float turnSpeed = 5.0f;
	float moveSpeed = 4.0f;
	float noise;
	float mouseTurnMultiplier = 1;

	private Animator _animator;
	private float Gravity = 20.0f;

	private float x;
	private float z;
	private float rotate;

	private Vector3 _moveDirection = Vector3.zero;
	private CharacterController _characterController;

	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		moveSpeed = 4.0f;
		noise = 5;
		_animator.speed = 1.0f;

		if (Input.GetButton ("Sneak")) {
			moveSpeed = 2.0f;
			noise = 2;
			_animator.speed = 0.75f;
		}

		z = Input.GetAxis ("Vertical");
		x = Input.GetAxis ("Horizontal");

		if (_characterController.isGrounded) {
			_moveDirection = new Vector3 (x, 0, z);
			_moveDirection = transform.TransformDirection (_moveDirection);
			_moveDirection *= moveSpeed;
		}

		_moveDirection.y -= Gravity * Time.deltaTime;
		_characterController.Move (_moveDirection * Time.deltaTime);

		_animator.SetBool("run", (z != 0 || x != 0));
		/*_moveDirection = new Vector3 (x, 0, z);

		Vector3 forward = Vector3.Scale(transform.forward, new Vector3(1,0,1)).normalized;
		Vector3 move = z * forward + x * transform.right;

		if (move.magnitude > 1f)
			move.Normalize ();

		move = transform.InverseTransformDirection (move);

		if (_characterController.isGrounded) {
			if (z > 0) _moveDirection = transform.forward * move.magnitude;
			if (z < 0) _moveDirection = -transform.forward * move.magnitude;
			if (x > 0) _moveDirection = transform.right * move.magnitude;
			if (x < 0) _moveDirection = -transform.right * move.magnitude;
			_moveDirection *= moveSpeed;
		}

		_moveDirection.y -= Gravity;

		_characterController.Move(_moveDirection * Time.deltaTime);*/

		rotate = Input.GetAxis ("Mouse X") * turnSpeed;
		transform.Rotate(new Vector3 (0, rotate, 0));
	}

	/*private float hspeed = 10f;
	private float vspeed = 0f;
	private float gravity = 1f;
	private float test = 0;
	private Vector3 moveDir = Vector3.zero;
	private float hDir;
	public bool isAccending = true;
	private float movSpeed = 0;

	public float movementSpeed = 10;
	public float turningSpeed = 60;

	void Update() {
		CharacterController controller = GetComponent<CharacterController> ();
		float h = Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime;
		float v = Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
		Vector3 movement = new Vector3 (-v, 0, h);

		controller.Move (movement);
		isAccending = controller.isGrounded;
		if (controller.isGrounded) {
			vspeed = 0;
		} else {
			vspeed -= gravity * Time.deltaTime;
		}

		controller.Move (new Vector3 (0, vspeed, 0));

		float rotate = Input.GetAxis ("Mouse X") * 5f;
		Debug.Log (rotate);
		transform.Rotate(new Vector3 (0, rotate, 0));
	}*/
}
