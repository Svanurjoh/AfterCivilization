using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

	private float attackDist = 1.5f;
	private float attackSpeed = 3.0f;
	private float headLevel = 1.5f;
	private float lastAttack;
	private float dist;
	private bool canAttack;
	private Animator _animator;
	private NavMeshAgent _agent;
	private Vector3 lastPost;
	private float lastMovement = 0;
	private float nextMovement = 10f;

	public GameObject projectile;
	public Transform SpawnLoc;

	void Start()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator>();
		lastPost = transform.position;
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
					//transform.LookAt(other.transform);
				}
			}

			dist = Vector3.Distance (tmpPlayer, tmpEnemy);

			if (dist <= attackDist && canAttack) {
				_animator.SetTrigger ("attack_1");
				lastAttack = 0;
				canAttack = false;
				var tempRot = SpawnLoc;
				tempRot.Rotate (0, 0, 90);
				Instantiate (projectile, SpawnLoc.position, tempRot.rotation);
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

		if (lastMovement > nextMovement) {
			NavMeshHit hit;
			var randomPoint = transform.position + Random.insideUnitSphere * 5f;
			if (NavMesh.SamplePosition (randomPoint, out hit, 8f, NavMesh.AllAreas)) {
				_agent.SetDestination (hit.position);
				nextMovement = Random.Range (8f, 20f);
			}
		}

		_animator.SetBool("run", lastPost != transform.position);
		lastMovement = (lastPost == transform.position) ? lastMovement += Time.deltaTime : 0;
		lastPost = transform.position;
	}
}
