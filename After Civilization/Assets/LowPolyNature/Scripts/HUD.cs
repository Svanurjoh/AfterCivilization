using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {


    public GameObject MessagePanel;

	public GameObject text;

	// Use this for initialization
	void Start () {
	}

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            // Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            // We found the empty slot
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                // Store a reference to the item
                itemDragHandler.Item = e.Item;

                break;
            }
        }
    }

    private bool mIsMessagePanelOpened = false;

    public bool IsMessagePanelOpened
    {
        get { return mIsMessagePanelOpened; }
    }

    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);

        mIsMessagePanelOpened = true;

        // TODO: set text when we will use this for other messages as well
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);

        mIsMessagePanelOpened = false;
    }

	void Update() {
		if (GameManagerScript.instance.getIsPaused ()) {
			text.SetActive (true);
		} else
			text.SetActive (false);
	}
}
