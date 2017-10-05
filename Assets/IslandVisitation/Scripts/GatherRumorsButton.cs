using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GatherRumorsButton : MonoBehaviour {

    public Button thisButton;

    public Text interactionsText;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClick);
	}
	
	private void OnClick()
    {
        interactionsText.text = PlayerStatus.VisitingIsland.GetRumors();
    }
}
