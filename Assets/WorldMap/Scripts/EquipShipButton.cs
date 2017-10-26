using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipShipButton : MonoBehaviour
{
    public Button thisButton;
    public GameObject thisPanel;
    public Text armamentName;
    public Text slot;

    private int invSlot;
    private ShipWeapon shipWeapon;
    private EquipShipManager managerScript;

    void Start()
    {
        managerScript = thisPanel.GetComponent<EquipShipManager>();
        thisButton.onClick.AddListener(OnClickEquip);
    }
    public void SetVars(int n) //n is the number button this is, which determines invSlot, and therefore the Weapon this refers to and the text
    {
        invSlot = n;
        if (PlayerStatus.Ship.weaponSlots[invSlot].equippedWeapon != null)
        {
            shipWeapon = PlayerStatus.Ship.weaponSlots[invSlot].equippedWeapon;
            armamentName.text = shipWeapon.GetName();
        }
        else armamentName.text = "[EMPTY" + PlayerStatus.Ship.weaponSlots[invSlot].validTypesString() + " Slot]";

        slot.text = "Slot " + (n + 1);
    }

    private void OnClickEquip()
    {
        managerScript.EquipArmament(invSlot);
    }
}
