using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

	public Transform Player;
	int MoveSpeed = 3;
	int MaxDist = 20;
	int attackDist = 1;

	public float Speed = 5.0f;
	public float RotationSpeed = 240.0f;
	private float Gravity = 20.0f;

	private float lastAttack;
	private float attackSpeed = 1.2f;
	private Animator _animator;
	private CharacterController _characterController;
	private Vector3 _moveDirection = Vector3.zero;
	private NavMeshAgent _agent;
	private Vector3 lastPost;
	private bool isInView = false;

	private float headLevel = 1.5f;

	void Start()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator>();
		_characterController = GetComponent<CharacterController>();
		lastPost = transform.position;
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			RaycastHit hit;

			Vector3 tmpEnemy = transform.position;
			Vector3 tmpPlayer = other.transform.position;
			tmpEnemy.y += headLevel;
			tmpPlayer.y += headLevel;

			//Debug.DrawRay (tmpEnemy, (tmpPlayer - tmpEnemy), Color.red, 2f, false);
			if (Physics.Raycast (tmpEnemy, (tmpPlayer - tmpEnemy), out hit, 10)) {
				if (hit.collider.tag == "Player") {
					_agent.destination = other.transform.position;
				}
			}

			float dist = Vector3.Distance (tmpPlayer, tmpEnemy);
			if (dist < attackDist) {
				_animator.SetBool("attack_1", dist < attackDist);
			}

		}
	}

	void Update()
	{
		bool isMoving = lastPost != transform.position;
		_animator.SetBool("run", isMoving);
		lastPost = transform.position;
	}
}
