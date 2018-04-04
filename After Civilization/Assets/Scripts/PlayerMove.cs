using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float turnSpeed = 5.0f;
	float moveSpeed = 4.0f;
	float noise;

	public AudioClip walking;
	public AudioClip running;

	private Animator _animator;
	private AudioSource _Walking;
	private AudioSource _Running;
	private AudioSource _Current;
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
		_Walking = AddAudio (walking, true, 1f, 0.75f);
		_Running = AddAudio (running, true, 1f, 0.75f);
		_Current = _Running;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManagerScript.instance.getIsPaused ()) {
			moveSpeed = 4.0f;
			noise = 1;
			_animator.speed = 1.0f;

			z = Input.GetAxis ("Vertical");
			x = Input.GetAxis ("Horizontal");

			if (x != 0 || z != 0) {
				noise = 4;
			}

			if (z < 0f) {
				moveSpeed /= 2;
				if (_Current != _Walking) {
					_Current.Stop ();
					_Current = _Walking;
				}
			} else {
				if (_Current != _Running) {
					_Current.Stop ();
					_Current = _Running;
				}
			}

			if (Input.GetButtonDown ("Sneak")) {
				_Current.Stop ();
				_Current = _Walking;
			} else if (Input.GetButtonUp ("Sneak")) {
				_Current.Stop ();
				_Current = _Running;
			} else if (Input.GetButton ("Sneak")) {
				if (_Current != _Walking) {
					_Current.Stop ();
					_Current = _Walking;
				}
			}

			if (Input.GetButton ("Sneak")) {
				moveSpeed = 2.0f;
				noise = 2;
				_animator.speed = 0.75f;
			}

			if (!_Current.isPlaying) {
				_Current.Play ();
			}
			if (x == 0 && z == 0) {
				_Current.Stop ();
			}


			if (_characterController.isGrounded) {
				_moveDirection = new Vector3 (x, 0, z);
				_moveDirection = transform.TransformDirection (_moveDirection);
				_moveDirection.Normalize ();
				_moveDirection *= moveSpeed;
			}

			_moveDirection.y -= Gravity * Time.deltaTime;
			_characterController.Move (_moveDirection * Time.deltaTime);

			_animator.SetBool ("run", (z != 0 || x != 0));

			rotate = Input.GetAxis ("Mouse X") * turnSpeed;
			transform.Rotate (new Vector3 (0, rotate, 0));
		}
	}

	public float GetNoise() {
		return noise;
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

	private AudioSource AddAudio(AudioClip clip, bool loop, float vol, float pitch) {
		var newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.volume = vol;
		newAudio.pitch = pitch;

		return newAudio;
	}
}
