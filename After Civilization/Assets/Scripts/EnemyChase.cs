using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

	private float attackDist = 1.5f;
	private float attackSpeed = 3.0f;
	private float moveAfterAttack = 0.5f;
	private float headLevel = 1.5f;
	private float agentSpeed = 4.5f;
	private int agentDamage = 1;
	private float shoutRadius = 10f;
	private float shoutCooldown = 3f;
	[HideInInspector]
	public bool canShout = false;
	private float lastShout = 3f;
	private float lastAttack;
	private float dist;
	private bool canAttack;
	private bool didAttacK;
	private Animator _animator;
	private NavMeshAgent _agent;
	private Vector3 lastPost;
	private bool isChasing = false;
	private AudioSource _audio;

	void Start()
	{
		_audio = GetComponent<AudioSource> ();
		_agent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator>();
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Vector3 tmpEnemy = transform.position;
			Vector3 tmpPlayer = other.transform.position;
			tmpEnemy.y += headLevel;
			tmpPlayer.y += headLevel;

			RaycastHit hit;
			int layerMask = ~(1 << 2);
			if (Physics.Raycast (tmpEnemy, (tmpPlayer - tmpEnemy), out hit, 10, layerMask)) {
				if (hit.collider.tag == "Player") {
					_agent.destination = other.transform.position;
					transform.LookAt(other.transform);
					CallForHelp (other.transform);
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
					GameManagerScript.instance.player.TakeDamage (agentDamage);
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
			_agent.speed = agentSpeed;
		}
		if (lastAttack >= attackSpeed) {
			canAttack = true;
		}
		lastShout += Time.deltaTime;
		isChasing = lastPost != transform.position ? true : false;
		_animator.SetBool("run", isChasing);
		lastPost = transform.position;
	}

	private void CallForHelp(Transform player) {
		if (canShout && lastShout >= shoutCooldown) {
			lastShout = 0f;
			_audio.Play ();
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

			for (var i = 0; i < enemies.Length; i++) {
				var enemy = enemies [i];
				if (Vector3.Distance (transform.position, enemy.transform.position) <= shoutRadius) {
					enemy.GetComponent<EnemyChase> ().AnswerHelp (player);
				}
			}
		}
	}

	public void AnswerHelp(Transform player) {
		if (!isChasing) {
			_agent.destination = player.position;
		}
	}
}
