using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restock : MonoBehaviour {

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
    public void OnClickResupply()
    {
        int cost = 100 - PlayerStatus.ResourcesCount;

        if (cost == 0) interactionsText.text = "You're already fully stocked!";
        else if(cost > PlayerStatus.GoldCount) interactionsText.text = "Not enough cash, stranger.";
        else
        {
            PlayerStatus.GoldCount -= cost;
            PlayerStatus.ResourcesCount = 100;

            goldCountText.text = "Gold: " + PlayerStatus.GoldCount.ToString();
            relevantStatText.text = "Resources: " + PlayerStatus.ResourcesCount.ToString();

            interactionsText.text = "Thanks fer buyin', stranger!";
        }
    }
}
