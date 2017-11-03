using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelManager : MonoBehaviour
{
    public GameObject adamButton; //The button which will be cloned to make all the dialogue buttons
    public GameObject dialoguePanel;
    public Text interactionsText;
    public GameObject mainPanel;

    public DialogueScript currentScript;

    private int currentSet;
    private bool[] actions;

    //void Start()
    //{
    //    currentScript = DialogueManager.dScript;

    //    Setup(0);
    //}

    //void OnEnable()
    //{
    //    currentScript = DialogueManager.dScript;

    //    Setup(0);
    //}

    public void Setup(int setNum)
    {
        if(setNum == 0)
        {
            actions = new bool[PlayerStatus.VisitingIsland.GetActions()];
        }

        if (currentScript == null) currentScript = DialogueManager.dScript;

        foreach (Transform child in dialoguePanel.transform) //Reset the panel
        {
            if (child.CompareTag("ToDestroy")) GameObject.Destroy(child.gameObject);
        }
        
        
        if ((setNum == currentScript.dialogueSets.Length)) //If the conclude-dialogue button is pressed, will close the dialogue panel and return to menu
        {
            PlayerStatus.VisitingIsland.TriggerDialogueConsequences(actions);

            gameObject.SetActive(false);
            PlayerStatus.VisitingIsland.GetActivePanel().SetActive(true);

            interactionsText.text = "";

            return;
        }
        interactionsText.text = currentScript.GetSetDialogue(setNum); //Set interactions panel text to the correct dialogue for the current set

        for (int i = 0; i < currentScript.GetSetSize(setNum); i++)
        {
            GameObject button = (GameObject)GameObject.Instantiate(adamButton); //Make a copy of the base dialogue button
            button.SetActive(true); //As the base button is not active, new button must be set active
            button.transform.SetParent(dialoguePanel.transform); //Set its parent to the dialogue panel
            button.tag = "ToDestroy"; //Mark the button for destruction on setup (the base button will not be destroyed this way)

            DialogueButtons dialogueButton = button.GetComponent<DialogueButtons>(); //Get a reference to its script
            dialogueButton.SetVars(currentScript.GetDialogue(setNum, i), currentScript.GetNextSet(setNum, i), currentScript.GetAction(setNum, i)); //Set the button's variables
        }
    }

    public void SetActions(int actionNum)
    {
        actions[actionNum] = true;
    }
}
