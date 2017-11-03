using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelManager : MonoBehaviour {

    public GameObject invLayoutPanel;
    public GameObject invItemButton;
    
    void OnEnable()
    {

        foreach (GameItem thisItem in PlayerStatus.VisitingIsland.GetShop().inventoryList)
        {
            if (thisItem.quantity != 0)
            {
                GameObject button = (GameObject)GameObject.Instantiate(invItemButton); //Make a copy of the base dialogue button
                button.SetActive(true); //As the base button is not active, new button must be set active
                button.transform.SetParent(invLayoutPanel.transform); //Set its parent to the dialogue panel
                button.tag = "ToDestroy";

                ShopItemButton inventoryItemButton = button.GetComponent<ShopItemButton>(); //Get a reference to its script
                if (inventoryItemButton == null) Debug.Log("script null");
                if (thisItem == null) Debug.Log("item null");
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
