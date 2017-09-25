using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TempSpecialButton : MonoBehaviour
{

    public Button testButton;
    public Text interactionsText;

    public string dScriptName;
    private string dScriptPath;

    public GameObject mainPanel;
    public GameObject dialoguePanel;

    public DialogueManager manager;

    private int nextSet;
    private int indicatedAction;

    private int killLoop;

    // Use this for initialization
    void Start()
    {
        dScriptPath = "Assets/DialogueTextFiles/" + dScriptName;
        testButton.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        mainPanel.gameObject.SetActive(false);

        manager.SetUpDialogue(dScriptPath);

        dialoguePanel.SetActive(true);
    }
}
