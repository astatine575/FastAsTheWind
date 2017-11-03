using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Text goldCount;
    public Text resourcesCount;

    public GameObject invLayoutPanel;
    public GameObject invItemButton;

	void OnEnable()
    {
        goldCount.text = PlayerStatus.GoldCount.ToString();
        resourcesCount.text = PlayerStatus.ResourcesCount.ToString();

        foreach (GameItem thisItem in PlayerStatus.Inventory.inventoryList)
        {

            if (thisItem.quantity != 0)
            {
                GameObject button = (GameObject)GameObject.Instantiate(invItemButton); //Make a copy of the base dialogue button
                button.SetActive(true); //As the base button is not active, new button must be set active
                button.transform.SetParent(invLayoutPanel.transform); //Set its parent to the dialogue panel
                button.tag = "ToDestroy";

                InventoryItemButton inventoryItemButton = button.GetComponent<InventoryItemButton>(); //Get a reference to its script
                inventoryItemButton.SetVars(thisItem);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in invLayoutPanel.transform) //Reset the panel
        {
            if (child.CompareTag("ToDestroy")) GameObject.Destroy(child.gameObject);
        }
    }
}
