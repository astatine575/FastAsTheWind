using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenuManager : MonoBehaviour
{
    public Text healthDisplay;
    public Text ammunitionsDisplay;

    public GameObject adamButton;

    public GameObject armamentsPanel;

    void OnEnable()
    {
        healthDisplay.text = PlayerStatus.ShipHealthCurrent + "/" + PlayerStatus.ShipHealthMax;
        ammunitionsDisplay.text = PlayerStatus.AmmoCount.ToString();

        for (int i = 0; i < PlayerStatus.Ship.weaponSlots.Length; i++)
        {

            GameObject button = (GameObject)GameObject.Instantiate(adamButton); //Make a copy of the base dialogue button
            button.SetActive(true); //As the base button is not active, new button must be set active
            button.transform.SetParent(armamentsPanel.transform); //Set its parent to the dialogue panel
            button.tag = "ToDestroy"; //Give it a tag to differentiate it from the adamButton

            ArmamentsButton inventoryItemButton = button.GetComponent<ArmamentsButton>(); //Get a reference to its script
            inventoryItemButton.SetVars(i);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in armamentsPanel.transform) //Reset the panel
        {
            if (child.CompareTag("ToDestroy")) GameObject.Destroy(child.gameObject);
        }
    }
}
