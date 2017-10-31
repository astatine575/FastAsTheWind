//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class BuyCannonballs : MonoBehaviour {

//    public int cannonballCost;

//    public Text interactionsText;
//    public Text relevantStatText;
//    public Text goldCountText;

//    private void Start()
//    {
//        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
//        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
//        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
//    }

//    public void OnClickBuyCannonballs()
//    {
//        int cost = cannonballCost * (20 - PlayerStatus.AmmoCount);

//        if (cost == 0) interactionsText.text = "Looks like you've already got more than enough heat, bud.";
//        else if (PlayerStatus.GoldCount < cost) interactionsText.text = "You tryin' to pull somethin', bud? You need more money for that!";
//        else
//        {
//            PlayerStatus.GoldCount -= cost;
//            PlayerStatus.AmmoCount = 20;

//            goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
//            relevantStatText.text = "Cannonballs: " + PlayerStatus.AmmoCount;
//            interactionsText.text = "You're mighty kind, you are.";
//        }
//    }
//}
