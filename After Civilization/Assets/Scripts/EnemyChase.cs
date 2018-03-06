using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

	private float attackDist = 1.5f;
	private float attackSpeed = 1.2f;
	private float headLevel = 1.5f;
	private float lastAttack;
	private float dist;
	private bool canAttack;
	private Animator _animator;
	private CharacterController _characterController;
	private NavMeshAgent _agent;
	private Vector3 lastPost;

	private int frameCount;
	private bool isAttacking;

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

			if (Physics.Raycast (tmpEnemy, (tmpPlayer - tmpEnemy), out hit, 10)) {
				if (hit.collider.tag == "Player") {
					_agent.destination = other.transform.position;
				}
			}

			dist = Vector3.Distance (tmpPlayer, tmpEnemy);
			transform.LookAt(other.transform);

			if (dist <= attackDist && canAttack) {
				lastAttack = 0;
				_animator.SetTrigger ("attack_1");
				canAttack = false;
				isAttacking = true;
				frameCount = Time.frameCount;
			}

			if (isAttacking && frameCount + 10 == Time.frameCount) {
				isAttacking = false;
				if (dist <= attackDist) {
					var player = other.GetComponent<PlayerController> ();
					player.TakeDamage (10);
				}
			}
		}
	}

	void Update()
	{
		if (!canAttack) {
			lastAttack += Time.deltaTime;
		}
		if (lastAttack >= attackSpeed) {
			canAttack = true;
		}

		_animator.SetBool("run", lastPost != transform.position);
		lastPost = transform.position;
	}
}
