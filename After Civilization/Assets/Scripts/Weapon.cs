using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	[HideInInspector]
	public bool isAttacking = false;
	public int damage = 10;
	public float attackSpeed = 1.2f;

	void Update() {
		transform.position += transform.forward;
		transform.Rotate (0, 29 * Time.deltaTime, 0);
	}
}
