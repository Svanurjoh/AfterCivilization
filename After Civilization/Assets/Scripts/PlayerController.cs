﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Animator _animator;
	private GameManagerScript GMS;
	private int frameCount = 0;
	private bool holdingGem = false;
    private HealthBar mHealthBar;
	private float lastAttack;
	private bool canAttack;
	private float attackSpeed = 3.0f;

	private Slider AxeCooldown;
	public int AxeCount = 20;
	public int Health = 100;
    public GameObject rightHand;
	public GameObject leftHand;
    public HUD Hud;
	public GameObject axe;
	public GameObject axeRot;
	private GameObject GemInArm = null;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
		AxeCooldown = GameObject.FindGameObjectWithTag ("AxeCooldown").GetComponent<Slider>();
    }

	void Awake() {
		mHealthBar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>();
		mHealthBar.Min = 0;
		mHealthBar.Max = Health;
		mHealthBar.SetHealth (Health);
	} 

    public void TakeDamage(int amount) {
        Health -= amount;
		if (Health <= 0) {
			Health = 0;
			isDead ();
		}

        mHealthBar.SetHealth(Health);
    }

    // Update is called once per frame
    void Update() {
		//Attack cooldown
		if (!canAttack) {
			lastAttack += Time.deltaTime;
			AxeCooldown.value = attackSpeed - lastAttack;
		}
		if (lastAttack >= attackSpeed) {
			canAttack = true;
		}
        // Throw axe
		if(Input.GetMouseButtonDown(0) && canAttack && AxeCount > 0) {
            _animator.SetTrigger("attack_1");
			canAttack = false;
			lastAttack = 0;
			frameCount = Time.frameCount;
			AxeCount--;
			GameObject.FindGameObjectWithTag ("AxeCounter").GetComponent<Text> ().text = AxeCount.ToString ();
        }
		if (frameCount + 6 == Time.frameCount) {
			throwAxe ();
		}

		//Holding a gem
		if(GemInArm != null)
			GemInArm.transform.position = leftHand.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Gem" && !holdingGem) {
			GemInArm = other.gameObject;
			holdingGem = true;
			GameManagerScript.instance.holdingGem.enabled = true;
			GameManagerScript.instance.gemPickedUp (GemInArm);
		}

		if (other.tag.Equals ("Weapon")) {
			var weapon = other.GetComponent<Weapon> ();
			if (weapon.isAttacking) {
				TakeDamage (weapon.damage);
				weapon.isAttacking = false;
			}
		}

		if (other.gameObject.tag == "Ship" && holdingGem) {
			GameManagerScript.instance.deliverGem (GemInArm);
			holdingGem = false;
			GemInArm = null;
			GameManagerScript.instance.holdingGem.enabled = false;
		}
    }

	public bool isHoldingGem() {
		return holdingGem;
	}

	private void throwAxe()
	{
		Instantiate (axe, rightHand.transform.position, Quaternion.Euler (-90, transform.localEulerAngles.y, -90));
		if (AxeCount <= 0) {
			GameObject.FindGameObjectWithTag ("EquipedAxe").gameObject.SetActive (true);
		}
	}

	private void isDead() {
		if (GemInArm != null) {
			GemInArm.transform.position = new Vector3 (100, 100, 100);
			holdingGem = false;
			GemInArm = null;
			GameManagerScript.instance.holdingGem.enabled = false;
		}
		Health = GameManagerScript.instance.GetPlayerHealth ();
		AxeCount = GameManagerScript.instance.GetPlayerAxes ();
		GameObject.FindGameObjectWithTag ("AxeCounter").GetComponent<Text> ().text = AxeCount.ToString ();
		mHealthBar.SetHealth (Health);
		GameManagerScript.instance.resetAllLevels ();
		this.transform.position = new Vector3 (24, 0, -10);
	}
}
