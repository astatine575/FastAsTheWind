using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmaarAttributes : IslandAttributes
{
    public string tavernRumor;
    private bool foundEllia;

    public string elliaFifty;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        hasSpecial = true;
        actions = 1;
    }
	
	public override bool CheckSpecial()
    {
        if (QuestsStatus.testQuestStatus >= 50)
        {
            specialVisible = true;
            return true;
        }
        else return false;
    }

    public override string GetRumors()
    {
        return tavernRumor;
    }

    public override string GetSpecInfoButtonText()
    {
        if (QuestsStatus.testQuestStatus == 40) return specInfo;
        return base.GetSpecInfoButtonText();
    }

    public override string GetSpecInfoResponse()
    {
        QuestsStatus.testQuestStatus = 50;
        return specInfoResponse;
    }

    public override void SpecialOnClick(MainPanelButton caller)
    {
        if (QuestsStatus.testQuestStatus == 50)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue("Assets/DialogueTextFiles/" + elliaFifty);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if(QuestsStatus.testQuestStatus == 100)
        {
            caller.interactionsText.text = "Sorry, but this store isn't open yet!";
        }
    }


    public override void TriggerDialogueConsequences(bool[] actions)
    {
        if (actions[0] == true)
        {
            ActionZero();
        }
    }

    public void ActionZero()
    {
        QuestsStatus.testQuestStatus = 100;
    }
}
