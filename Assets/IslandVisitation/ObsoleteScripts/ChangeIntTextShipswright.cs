using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIntTextShipswright : MonoBehaviour {

    public Text interactionsText;
    public Text relevantStatText;
    public Text goldCountText;

    private void Start()
    {
        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
    }

    public void OnClickChangeText()
    {
        if (PlayerStatus.Ship._hullHealth == PlayerStatus.Ship.HullStrengthMax())
            interactionsText.text = "Looking to upgrade your ship, eh?";
        else interactionsText.text = "Yer ship's lookin' a little rough there. " +
                "Want me to patch 'er up fer ya?";

        goldCountText.text = "Gold: " + PlayerStatus.GoldCount.ToString();
        relevantStatText.text = "Hull Health: " + PlayerStatus.Ship._hullHealth + "/" + PlayerStatus.Ship.HullStrengthMax();
    }
}
