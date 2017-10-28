using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValenciaAttributes : IslandAttributes
{
    public string tavernRumor;

    public TextAsset aldrennZero;
    public TextAsset aldrennTen;
    public TextAsset aldrennTwenty;
    public TextAsset aldrennThirty;
    public TextAsset aldrennFortyPlus;
    public TextAsset aldrennHundred;
    public TextAsset aldrennNegTen;

    private DialogueManager manager;


    public override void Start()
    {
        base.Start();
        hasSpecial = true;
        SetActions(5);
    }

    public override bool CheckSpecial()
    {
        if (QuestsStatus.testQuestStatus != -1)
        {
            specialVisible = true;
            return true;
        }
        else return false;
    }

    public override string GetRumors()
    {
        QuestsStatus.testQuestStatus = 0;
        return tavernRumor;
    }

    public override void SpecialOnClick(MainPanelButton caller)
    {
        if (QuestsStatus.testQuestStatus == 0)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennZero);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus == 10)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennTen);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus == 20)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennTwenty);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus == 30)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennThirty);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus >= 40)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennFortyPlus);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus == 100)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennHundred);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }

        if (QuestsStatus.testQuestStatus == -10)
        {
            caller.mainPanel.gameObject.SetActive(false);

            DialogueManager.SetUpDialogue(aldrennNegTen);

            DialoguePanelManager dpm = caller.dialoguePanel.GetComponent<DialoguePanelManager>();
            //dpm.currentScript = DialogueManager.dScript;

            caller.dialoguePanel.SetActive(true);
            dpm.Setup(0);
        }
    }

    public override void TriggerDialogueConsequences(bool[] actions)
    {
        if (actions[0] == true)
        {
            ActionZero();
        }

        if (actions[1] == true)
        {
            ActionOne();
        }

        if (actions[2] == true)
        {
            ActionTwo();
        }

        if (actions[3] == true)
        {
            ActionThree();
        }

        if (actions[4] == true)
        {
            ActionFour();
        }

        //if (actions[5] == true)
        //{
        //    ActionFive();
        //}

        //if (actions[6] == true)
        //{
        //    ActionSix();
        //}

        //if (actions[7] == true)
        //{
        //    ActionSeven();
        //}
    }

    private void ActionZero()
    {
        QuestsStatus.testQuestStatus = 10;
    }

    private void ActionOne()
    {
        QuestsStatus.testQuestStatus = 20;
    }

    private void ActionTwo()
    {
        QuestsStatus.testQuestStatus = -10;
    }

    private void ActionThree()
    {
        QuestsStatus.testQuestStatus = 30;
    }

    private void ActionFour()
    {
        QuestsStatus.testQuestStatus = 40;
    }

    //private void ActionFive()
    //{

    //}

    //private void ActionSix()
    //{

    //}

    //private void ActionSeven()
    //{

    //}

}
