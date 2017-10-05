using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecificInfoButton : MonoBehaviour {

    public Button thisButton;
    public Text buttonText;

    public Text interactionsText;

    // Use this for initialization
    void Start()
    {
        buttonText.text = PlayerStatus.VisitingIsland.GetSpecInfoButtonText();
        thisButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        interactionsText.text = PlayerStatus.VisitingIsland.GetSpecInfoResponse();
    }
}
