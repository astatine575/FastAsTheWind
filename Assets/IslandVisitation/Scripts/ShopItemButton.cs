using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public Button thisButton;
    public Text itemName;

    public GameObject thisPanel;
    public GameObject purchasePanel;

    private GameItem refItem;

    // Use this for initialization
    void Start()
    {
        thisButton.onClick.AddListener(OnClickDetails);
    }

    public void SetVars(GameItem item)
    {
        //Set itemName.text to itemObject's name, and itemQuantity.text to "x" + itemObject's quantity
        refItem = item;
        itemName.text = item.GetName();
    }

    private void OnClickDetails()
    {
        thisPanel.SetActive(false);

        GameItemPurchaseManager purchaseScript = purchasePanel.gameObject.GetComponent<GameItemPurchaseManager>();
        purchaseScript.purchasedItem = refItem;
        purchaseScript.returnPanel = thisPanel;
        purchasePanel.SetActive(true);
        //Once inventory is implemented, find a way to pass the button's stored object to the details panel
    }
}