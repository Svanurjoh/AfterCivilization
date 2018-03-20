using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public GameObject rightHand;
	public GameObject leftHand;
    public HUD Hud;
	public GameObject axe;
	public GameObject axeRot;
	private GameObject GemInArm = null;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();

        mHealthBar = Hud.transform.Find("HealthBar").GetComponent<HealthBar>();
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
		mHealthBar.SetHealth (2);
    }

    public int Health = 100;

    public void TakeDamage(int amount)
    {
        Health -= amount;
		if (Health <= 0) {
			Health = 0;
			var scene = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);
		}

        mHealthBar.SetHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
		//Attack cooldown
		if (!canAttack) {
			lastAttack += Time.deltaTime;
		}
		if (lastAttack >= attackSpeed) {
			canAttack = true;
		}
        // Throw axe
		if(Input.GetMouseButtonDown(0) && canAttack)
        {
            _animator.SetTrigger("attack_1");
			canAttack = false;
			lastAttack = 0;
			frameCount = Time.frameCount;
        }
		if (frameCount + 14 == Time.frameCount) {
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
		}
    }

	public bool isHoldingGem() {
		return holdingGem;
	}

	private void throwAxe()
	{
		Instantiate (axe, rightHand.transform.position, Quaternion.Euler(-90, transform.localEulerAngles.y, -90));
	}
}
