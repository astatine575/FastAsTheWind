using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsManager : MonoBehaviour
{
    public Text detailsText;
    public GameItem gameItem;

    public GameObject thisPanel;
    public GameObject crewEquip;
    public GameObject shipEquip;

    public Button equipButton;
    public Button dropButton;

    [HideInInspector]
    public GameObject returnPanel;
    public bool isEquipped = false;

    void OnEnable()
    {
        detailsText.text = gameItem.GetAttributes();
        if (((gameItem.GetItemType() == "Personal Weapon") || (gameItem.GetItemType() == "Ship Weapon")) && !isEquipped)
        {
            if (gameItem.GetItemType() != "Personal Weapon") equipButton.gameObject.SetActive(true);
            else equipButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(true);
        }
        else
        {
            equipButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        isEquipped = false;
    }

    public void Equip()
    {
        thisPanel.SetActive(false);

        GameObject target = null;
        if (gameItem.GetItemType() == "Personal Weapon") target = crewEquip;
        else if (gameItem.GetItemType() == "Ship Weapon")
        {
            target = shipEquip;

            EquipShipManager script = target.GetComponent<EquipShipManager>();
            script.weaponEquipping = (ShipWeapon)gameItem;
        }

        if (!target) return;

        target.SetActive(true);
    }

    public void Back()
    {
        returnPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
