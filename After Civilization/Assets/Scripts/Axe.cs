using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : InventoryItemBase {

	private bool isTouching = false;
	private PlayerController _playerController;

	void Awake() {
		_playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}

    public override string Name
    {
        get
        {
            return "Axe";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyMesh" && _playerController.getSwing()) {
			Debug.Log (_playerController.getSwing ());
			Destroy (other.gameObject);
		}
	}

	public bool touching()
	{
		return isTouching;
	}
}
