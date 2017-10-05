using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIntTextEnter : MonoBehaviour {

    public string enterText;

    public string displayedStat; //use "Resources", "Cannonballs", or "Health"

    private Text interactionsText;
    private Text relevantStatText;
    private Text goldCountText;

    private void Start()
    {
        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
    }

    // Use this for initialization
    public void OnClickChangeText()
    {
        interactionsText.text = enterText;
        goldCountText.text = "Gold: " + PlayerStatus.GoldCount.ToString();

        string statDisplay;

       if (displayedStat == "Resources")
        {
            statDisplay = PlayerStatus.ResourcesCount.ToString();
        }
        else if (displayedStat == "Cannonballs")
        {
            statDisplay = PlayerStatus.AmmoCount.ToString();
        }
        else statDisplay = "error";


        relevantStatText.text = displayedStat + ": " + statDisplay;
    }
}
