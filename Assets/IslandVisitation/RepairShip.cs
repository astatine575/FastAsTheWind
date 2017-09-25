﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairShip : MonoBehaviour {

    public Text interactionsText;
    public Text goldCountText;
    public Text relevantStatText;

    private void Start()
    {
        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
    }

    // Use this for initialization
    public void OnClickRepairShip()
    {
        int cost = PlayerStatus.ShipHealthMax - PlayerStatus.ShipHealthCurrent;

        if (PlayerStatus.ShipHealthCurrent == PlayerStatus.ShipHealthMax)
            interactionsText.text = "She looks fine to me, mate.";
        else if (cost > PlayerStatus.GoldCount) interactionsText.text = "Come back when you have some gold." +
                "I don't work for free, you know.";
        else
        {
            PlayerStatus.GoldCount -= cost;
            PlayerStatus.ShipHealthCurrent = PlayerStatus.ShipHealthMax;

            goldCountText.text = "Gold: " + PlayerStatus.GoldCount.ToString();
            relevantStatText.text = "Ship Health: " + PlayerStatus.ShipHealthCurrent + "/" + PlayerStatus.ShipHealthMax;

            interactionsText.text = "There you go. Good as new.";
        }
    }
}
