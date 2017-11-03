using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameItemPurchaseManager : MonoBehaviour
{

    public Text itemBuying;
    public Text quantityBuyingText;
    public Text goldCount;
    public Text purchaseCost;

    public GameObject thisPanel;

    private int quantityBuying;
    private int purchasePrice;

    [HideInInspector]
    public GameItem purchasedItem;
    public GameObject returnPanel;
    public Text quantityPossessed;

    private void OnEnable()
    {
        if (itemBuying == null) Debug.Log("text null");
        if (purchasedItem == null) Debug.Log("item null");
        itemBuying.text = "How many " + purchasedItem.GetName() + "s would you like to buy?";

        quantityBuying = 0;
        quantityBuyingText.text = quantityBuying.ToString();

        purchasePrice = 0;
        purchaseCost.text = "Cost: " + purchasePrice;
    }

    private void OnDisable()
    {
        goldCount.text = "Gold: " + PlayerStatus.GoldCount;
    }

    public void AlterQuantity(int quantity)
    {
        quantityBuying += quantity;

        purchasePrice = quantityBuying * purchasedItem.GetValue();

        //if(purchasePrice > PlayerStatus.GoldCount)
        //{
        //    int disparity = purchasePrice - PlayerStatus.GoldCount;

        //    double extraItemsUnrounded = disparity / purchasedItem.GetValue();
        //    int extraItems = (int)Math.Ceiling(extraItemsUnrounded);
        //    Debug.Log(extraItems.ToString());

        //    quantityBuying -= extraItems;
        //    purchasePrice = quantityBuying * purchasedItem.GetValue();
        //}

        while(purchasePrice > PlayerStatus.GoldCount)
        {
            quantityBuying--;
            purchasePrice = quantityBuying * purchasedItem.GetValue();
        }

        if (quantityBuying < 0)
        {
            quantityBuying = 0;
            purchasePrice = 0;
        }

        quantityBuyingText.text = quantityBuying.ToString();
        purchaseCost.text = "Cost: " + purchasePrice;
    }

    public void Purchase()
    {
        PlayerStatus.GoldCount -= purchasePrice;
        goldCount.text = "Gold: " + PlayerStatus.GoldCount;

        for(int i = 0; i < quantityBuying; i++)
        {
            PlayerStatus.Inventory.AddItem(purchasedItem);
        }

        thisPanel.SetActive(false);
        returnPanel.SetActive(true);
    }
}
