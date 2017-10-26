using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipShipManager : MonoBehaviour
{
    public GameObject layoutPanel;
    public GameObject adamButton;
    public GameObject returnPanel;

    [HideInInspector]
    public ShipWeapon weaponEquipping;

    void OnEnable()
    {
        for (int i = 0; i < PlayerStatus.Ship.weaponSlots.Length; i++)
        {

            GameObject button = (GameObject)GameObject.Instantiate(adamButton); //Make a copy of the base dialogue button
            button.SetActive(true); //As the base button is not active, new button must be set active
            button.transform.SetParent(layoutPanel.transform); //Set its parent to the dialogue panel
            button.tag = "ToDestroy"; //Give it a tag to differentiate it from the adamButton

            EquipShipButton inventoryItemButton = button.GetComponent<EquipShipButton>(); //Get a reference to its script
            inventoryItemButton.SetVars(i);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in layoutPanel.transform) //Reset the panel
        {
            if (child.CompareTag("ToDestroy")) GameObject.Destroy(child.gameObject);
        }
    }

    public void EquipArmament(int invSlot)
    {
        if (!PlayerStatus.Ship.weaponSlots[invSlot].validWeapon(weaponEquipping)) return;
        PlayerStatus.Ship.weaponSlots[invSlot].Replace(weaponEquipping);

        //Debug.Log("Inventory:");
        //for (int i = 0; i < PlayerStatus.Inventory.inventoryList.Count; i++)
        //    Debug.Log(PlayerStatus.Inventory.inventoryList[i].quantity + " " + PlayerStatus.Inventory.inventoryList[i].GetName());
        //Debug.Log("Ship stuff:");
        //for (int i = 0; i < PlayerStatus.Ship.weaponSlots.Length; i++)
        //    Debug.Log(PlayerStatus.Ship.weaponSlots[i].equippedWeapon.GetName());

        returnPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
