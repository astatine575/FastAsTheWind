using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainPanelButton : MonoBehaviour
{

    public Button thisButton;
    public Text buttonText;
    private MainPanelButton buttonRef;

    public Text interactionsText;
    public Text relevantStatText;
    public Text goldCountText;

    public GameObject mainPanel;
    public GameObject linkedPanel;

    public GameObject dialoguePanel;
    private DialogueManager manager;


    // Use this for initialization
    void Start ()
    {
        buttonRef = (MainPanelButton)thisButton.GetComponent<MainPanelButton>();
        if (thisButton.CompareTag("Special")) buttonText.text = PlayerStatus.VisitingIsland.specialText;

        //Find all the private game object references so it doesn't have to be done in the editor
        interactionsText = GameObject.Find("InteractionsText").GetComponent<Text>();
        relevantStatText = GameObject.Find("RelevantStatCount").GetComponent<Text>();
        goldCountText = GameObject.Find("GoldCount").GetComponent<Text>();
        //mainPanel = GameObject.Find("MainPanel").GetComponent<GameObject>();
        //marketPanel = GameObject.Find("MarketPanel").GetComponent<GameObject>();
        //shipswrightPanel = GameObject.Find("ShipswrightPanel").GetComponent<GameObject>();
        //tavernPanel = GameObject.Find("TavernPanel").GetComponent<GameObject>();
        //blacksmithPanel = GameObject.Find("BlacksmithPanel").GetComponent<GameObject>();
        //gunsmithPanel = GameObject.Find("GunsmithPanel").GetComponent<GameObject>();
        //archetierPanel = GameObject.Find("ArchetierPanel").GetComponent<GameObject>();
        //wardsmithPanel = GameObject.Find("WardsmithPanel").GetComponent<GameObject>();
        //dialoguePanel = GameObject.Find("DialoguePanel").GetComponent<GameObject>();

        thisButton.onClick.AddListener(OnClick);
    }


    public DialogueManager GetManager()
    {
        return manager;
    }
	
	private void OnClick()
    {
        if(thisButton.CompareTag("Market")) //This calls another method so that, in the case of islands with special events, those methods can be written over
        {
            PlayerStatus.VisitingIsland.MarketOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Shipswright"))
        {
            PlayerStatus.VisitingIsland.ShipswrightOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Tavern"))
        {
            PlayerStatus.VisitingIsland.TavernOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Blacksmith"))
        {
            PlayerStatus.VisitingIsland.BlacksmithOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Gunsmith"))
        {
            PlayerStatus.VisitingIsland.GunsmithOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Archetier"))
        {
            PlayerStatus.VisitingIsland.ArchetierOnClick(buttonRef);
        }

        if (thisButton.CompareTag("Wardsmith"))
        {
            PlayerStatus.VisitingIsland.WardsmithOnClick(buttonRef);
        }

        if(thisButton.CompareTag("Special"))
        {
            PlayerStatus.VisitingIsland.SpecialOnClick(buttonRef);
        }

        if (thisButton.CompareTag("SetSail"))
        {
            PlayerStatus.VisitingIsland.SetSailOnClick(buttonRef);
        }
    }
}
