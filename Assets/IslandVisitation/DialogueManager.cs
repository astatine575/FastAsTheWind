using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DialogueScript //A script contains an array of DialogueSet class objects
{
    public DialogueSet[] dialogueSets;


    public int GetSetSize(int set)
    {
        return dialogueSets[set].choiceSets.Length;
    }

    public string GetDialogue(int setNum, int optionNum)
    {
        return dialogueSets[setNum].choiceSets[optionNum].dialogue;
    }

    public string GetSetDialogue(int setNum)
    {
        return dialogueSets[setNum].npcDialogue;
    }

    public int GetNextSet(int setNum, int optionNum)
    {
        return dialogueSets[setNum].choiceSets[optionNum].nextSet;
    }

    public int GetAction(int setNum, int optionNum)
    {
        return dialogueSets[setNum].choiceSets[optionNum].executedAction;
    }
}

public class DialogueSet //A dialogue set contains a string for npc dialogue and an array of DAChoice class objects
{
    public string npcDialogue;
    public DAChoice[] choiceSets;
}

public class DAChoice //A DAChoice contains a string for player's potential dialogue, an int for the next set, and an int to indicate the to-be-executed action
{
    public string dialogue;
    public int nextSet;
    public int executedAction; 
}

public class DialogueManager : MonoBehaviour
{

    public static DialogueScript dScript = new DialogueScript();
    public static int[] dActions = new int[1]; //Unless I find a better way to do this, this number will have to be changed whenever there a new dialogue-resultant method is made

	// Use this for initialization
	void Start ()
    {

    }

    
    public void SetUpDialogue(string filePath)
    {
        string path = filePath;
        StreamReader dialogueReader = new StreamReader(path);

        string dialogueSizeString = GetElement(dialogueReader);
        int dialogueSize = Int32.Parse(dialogueSizeString);
        DialogueSet[] dSetArray = new DialogueSet[dialogueSize]; //This is the one variable of the public static DialogueScript class object


        for (int i = 0; i < dSetArray.Length; i++) //For each spot in the dSetArray, create and add a dialogue set to dSetArray
        {

            string gameDialogue = GetElement(dialogueReader);


            string choiceSetSizeString = GetElement(dialogueReader);
            int choiceSetSize = Int32.Parse(choiceSetSizeString);
            DAChoice[] choiceSet = new DAChoice[choiceSetSize];



            for(int j = 0; j < choiceSet.Length; j++) //For each dialogue choice in the set, create and add a DAChoice to choiceSet
            {

                DAChoice thisChoice = new DAChoice();

                string choiceDialogue = GetElement(dialogueReader);
                thisChoice.dialogue = choiceDialogue;
                

                string nextSetString = GetElement(dialogueReader);
                thisChoice.nextSet = Int32.Parse(nextSetString);
                

                string actionString = GetElement(dialogueReader);
                if (actionString == "-1") thisChoice.executedAction = -1;
                else thisChoice.executedAction = Int32.Parse(actionString);

                choiceSet[j] = thisChoice;
            }
            DialogueSet thisDialogueSet = new DialogueSet(); //Done making the dialogue/action choice array, so we can create the dialogue set, set up the values, then add it
            thisDialogueSet.choiceSets = choiceSet;
            thisDialogueSet.npcDialogue = gameDialogue;
            dSetArray[i] = thisDialogueSet;
        }

        dScript.dialogueSets = dSetArray; //Done making all sets, so dScript's dialogueSets can be set to dSetArray
    }

    public string GetElement(StreamReader reader)
    {
        string element = "";
        char nextChar = (char)reader.Read();

        while (((nextChar == '/') || (nextChar == '\n') || (nextChar == ' ')) && (reader.Peek() != -1)) //Finds start of element (forward slashes, spaces, and line breaks are ignored)
        {
            nextChar = (char)reader.Read();
        }

        while ((nextChar != '/') && (nextChar != '\n')) //Reads element
        {
            element += nextChar;

            if (reader.Peek() == -1) break; //Prevents infinite loops at the conclusion of reading the .txt file

            nextChar = (char)reader.Read();
        }
        return element;
    }
	
	public void ClearScript()
    {
        dScript = null;
    }
}
