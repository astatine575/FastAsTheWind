using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentsButton : MonoBehaviour {

    public Button thisButton;
    public Text armamentName;
    public Text slot;

    public GameObject thisPanel;
    public GameObject detailsPanel;

    private int invSlot;
    private ShipWeapon shipWeapon;

    // Use this for initialization
    void Start()
    {
        thisButton.onClick.AddListener(OnClickDetails);
    }

    public void SetVars(int n)
    {
        invSlot = n;
        if (PlayerStatus.Ship.weaponSlots[invSlot].equippedWeapon != null)
        {
            shipWeapon = PlayerStatus.Ship.weaponSlots[invSlot].equippedWeapon;
            armamentName.text = shipWeapon.GetName();
        }
        else armamentName.text = "[EMPTY]";

        slot.text = "Slot " + (n + 1);
    }

    private void OnClickDetails()
    {
        if (PlayerStatus.Ship.weaponSlots[invSlot].equippedWeapon == null) return;

        thisPanel.SetActive(false);

        //Getting a reference to the script of the details panel, then setting the GameItem, isEquipped, and the return panel
        ItemDetailsManager detailsScript = detailsPanel.gameObject.GetComponent<ItemDetailsManager>();
        detailsScript.gameItem = (GameItem)shipWeapon;
        detailsScript.isEquipped = true;
        detailsScript.returnPanel = thisPanel;

        detailsPanel.SetActive(true);
        //Once inventory is implemented, find a way to pass the button's stored object to the details panel
    }
}
