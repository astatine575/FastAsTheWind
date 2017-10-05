using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeIntTextBack : MonoBehaviour {

    public Text interactionsText;
    public Text relevantStatText;
    public Text goldCountText;

    private void Start()
    {
        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
    }

    // Use this for initialization
    public void OnClickChangeText()
    {
        interactionsText.text = "Where to next, captain?";
        relevantStatText.text = "";
        goldCountText.text = "";
    }
}
