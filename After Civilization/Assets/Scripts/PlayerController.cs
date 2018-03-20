using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private IInventoryItem mCurrentItem = null;
	private GameManagerScript GMS;

    private bool mLockPickup = false;
	private int frameCount = 0;
	private bool isSwinging = false;
	private bool holdingGem = false;
    private HealthBar mHealthBar;

    #endregion

    #region Public Members

    public Inventory inventory;

    public GameObject rightHand;
	public GameObject leftHand;

    public HUD Hud;

	public GameObject axe;
	public GameObject axeRot;
	private GameObject GemInArm = null;

    #endregion

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;

        mHealthBar = Hud.transform.Find("HealthBar").GetComponent<HealthBar>();
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
		mHealthBar.SetHealth (2);
    }

    #region Inventory

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

    }

    private void SetItemActive(IInventoryItem item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
		currentItem.transform.parent = active ? rightHand.transform : null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        // If the player carries an item, un-use it (remove from player's hand)
        if(mCurrentItem != null)
        {
            SetItemActive(mCurrentItem, false);
        }

        IInventoryItem item = e.Item;

        // Use item (put it to hand of the player)
        SetItemActive(item, true);

        mCurrentItem = e.Item;
    }

    private void DropCurrentItem()
    {
        mLockPickup = true;

        _animator.SetTrigger("tr_drop");

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        inventory.RemoveItem(mCurrentItem);

        // Throw animation
        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }

    }

    public void DoDropItem()
    {
        mLockPickup = false;

        // Remove Rigidbody
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

        mCurrentItem = null;
    }

    #endregion

    #region Health

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

    #endregion


    void FixedUpdate()
    {
        // Drop item
        if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
        {
            DropCurrentItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pickup item
        if(mItemToPickup != null && Input.GetKeyDown(KeyCode.F))
        {
            inventory.AddItem(mItemToPickup);
            mItemToPickup.OnPickup();
            Hud.CloseMessagePanel();

            mItemToPickup = null;
        }

        // Execute action with item
		if(Input.GetMouseButtonDown(0) && !isSwinging)
        {
            // TODO: Logic which action to execute has to come from the particular item
			isSwinging = true;
			frameCount = Time.frameCount;
            _animator.SetTrigger("attack_1");
        }

		if (frameCount + 14 == Time.frameCount) {
			throwAxe ();
			isSwinging = false;
		}

		if(GemInArm != null)
			GemInArm.transform.position = leftHand.transform.position;
    }

    private IInventoryItem mItemToPickup = null;

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

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mItemToPickup = null;
        }
    }

	public bool getSwing() {
		return isSwinging;
	}

	public bool isHoldingGem() {
		return holdingGem;
	}

	private void throwAxe()
	{
		Instantiate (axe, rightHand.transform.position, Quaternion.Euler(-90, transform.localEulerAngles.y, -90));
	}
}
