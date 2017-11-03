using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPurchaseManager : MonoBehaviour
{
    public int resourcesCost;

    public Text itemBuying;
    public Text quantityBuyingText;
    public Text quantityPossessed;
    public Text goldCount;
    public Text relevantStatCount;
    public Text purchaseCost;

    public GameObject thisPanel;
    public GameObject returnPanel;

    private int quantityBuying;
    private int purchasePrice;

    private void OnEnable()
    {
        itemBuying.text = "How much would you like?";

        quantityBuying = 0;
        quantityBuyingText.text = quantityBuying.ToString();

        purchasePrice = 0;
        purchaseCost.text = "Cost: " + purchasePrice;
    }

    private void OnDisable()
    {
        goldCount.text = "Gold: " + PlayerStatus.GoldCount;
        relevantStatCount.text = "Resources: " + PlayerStatus.ResourcesCount;
    }

    public void AlterQuantity(int quantity)
    {
        quantityBuying += quantity;

        purchasePrice = quantityBuying * resourcesCost;

        if(purchasePrice > PlayerStatus.GoldCount)
        {
            int disparity = purchasePrice - PlayerStatus.GoldCount;

            double extraItemsUnrounded = disparity / resourcesCost;
            int extraItems = (int)Math.Ceiling(extraItemsUnrounded);

            quantityBuying -= extraItems;
            purchasePrice -= extraItems * resourcesCost;
        }

        if(quantityBuying < 0)
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
        PlayerStatus.ResourcesCount += quantityBuying;

        thisPanel.SetActive(false);
        returnPanel.SetActive(true);
    }
}
