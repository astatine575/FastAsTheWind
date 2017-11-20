using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public Button thisButton;
    public Text itemName;
    public Text itemQuantity;

    public GameObject thisPanel;
    public GameObject detailsPanel;

    private GameItem refItem;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickDetails);
	}
	
	public void SetVars(GameItem item)
    {
        //Set itemName.text to itemObject's name, and itemQuantity.text to "x" + itemObject's quantity
        refItem = item;
        itemName.text = item.GetName();
        if (item.GetStackable()) itemQuantity.text = "x" + item.quantity;
        else itemQuantity.text = "";
    }

    private void OnClickDetails()
    {
        thisPanel.SetActive(false);

        ItemDetailsManager detailsScript = detailsPanel.gameObject.GetComponent<ItemDetailsManager>();
        detailsScript.gameItem = refItem;
        detailsScript.returnPanel = thisPanel;
        detailsPanel.SetActive(true);
        //Once inventory is implemented, find a way to pass the button's stored object to the details panel
    }
}
