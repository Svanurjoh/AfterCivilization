using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

	private float attackDist = 1.5f;
	private float attackSpeed = 3.0f;
	private float moveAfterAttack = 0.5f;
	private float headLevel = 1.5f;
	private float lastAttack;
	private float dist;
	private bool canAttack;
	private bool didAttacK;
	private Animator _animator;
	private NavMeshAgent _agent;
	private Vector3 lastPost;
	//private float lastMovement = 0;
	//private float nextMovement = 10f;

	//public GameObject projectile;
	//public Transform SpawnLoc;

	void Start()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator>();
		//lastPost = transform.position;
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Vector3 tmpEnemy = transform.position;
			Vector3 tmpPlayer = other.transform.position;
			tmpEnemy.y += headLevel;
			tmpPlayer.y += headLevel;

			RaycastHit hit;
			if (Physics.Raycast (tmpEnemy, (tmpPlayer - tmpEnemy), out hit, 10)) {
				if (hit.collider.tag == "Player") {
					_agent.destination = other.transform.position;
					transform.LookAt(other.transform);
				}
			}

			dist = Vector3.Distance (tmpPlayer, tmpEnemy);

			if (dist <= attackDist && canAttack) {
				_animator.SetTrigger ("attack_1");
				_agent.speed = 0;
				lastAttack = 0;
				canAttack = false;
				didAttacK = true;

			}
			if (lastAttack >= moveAfterAttack && didAttacK) {
				didAttacK = false;
				if (dist <= attackDist) {
					GameManagerScript.instance.player.TakeDamage (1);
				}
			}
		}
	}

	void Update()
	{
		if (!canAttack) {
			lastAttack += Time.deltaTime;
		}
		if (lastAttack >= moveAfterAttack) {
			_agent.speed = 4.5f;
		}
		if (lastAttack >= attackSpeed) {
			canAttack = true;
		}


		/*if (lastMovement > nextMovement) {
			NavMeshHit hit;
			var randomPoint = transform.position + Random.insideUnitSphere * 5f;
			if (NavMesh.SamplePosition (randomPoint, out hit, 8f, NavMesh.AllAreas)) {
				_agent.SetDestination (hit.position);
				nextMovement = Random.Range (8f, 20f);
			}
		}*/

		_animator.SetBool("run", lastPost != transform.position);
		//lastMovement = (lastPost == transform.position) ? lastMovement += Time.deltaTime : 0;
		lastPost = transform.position;
	}
}
