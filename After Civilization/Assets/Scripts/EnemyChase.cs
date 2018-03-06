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
	private bool isChasing;
	private Animator _animator;
	private NavMeshAgent _agent;
	private Vector3 lastPost;

	private int frameCount;
	private bool isAttacking;

	void Start()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator>();
		lastPost = transform.position;
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			isChasing = false;
			Vector3 tmpEnemy = transform.position;
			Vector3 tmpPlayer = other.transform.position;
			tmpEnemy.y += headLevel;
			tmpPlayer.y += headLevel;

			RaycastHit hit;
			if (Physics.Raycast (tmpEnemy, (tmpPlayer - tmpEnemy), out hit, 10)) {
				if (hit.collider.tag == "Player") {
					isChasing = true;
					_agent.destination = other.transform.position;
				}
			}

			dist = Vector3.Distance (tmpPlayer, tmpEnemy);
			transform.LookAt(other.transform);

			if (dist <= attackDist && canAttack) {
				_animator.SetTrigger ("attack_1");
				lastAttack = 0;
				canAttack = false;
				isAttacking = true;
				frameCount = Time.frameCount;
			}

			if (isAttacking && frameCount + 12 == Time.frameCount) {
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

		if (_agent.remainingDistance <= 1f && !isChasing) {
			NavMeshHit hit;
			var randomPoint = transform.position + Random.insideUnitSphere * 5f;
			if (NavMesh.SamplePosition (randomPoint, out hit, 2f, NavMesh.AllAreas)) {
				_agent.SetDestination (hit.position);
			}
		}

		_animator.SetBool("run", lastPost != transform.position);
		lastPost = transform.position;
	}
}
