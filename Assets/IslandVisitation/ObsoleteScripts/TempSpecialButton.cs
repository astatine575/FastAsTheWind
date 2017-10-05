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

    private DialogueManager manager;

    private int nextSet;
    private int indicatedAction;
    

    // Use this for initialization
    //void Start()
    //{
        
    //    testButton.onClick.AddListener(OnClick);
    //}


    //private void OnClick()
    //{
    //    mainPanel.gameObject.SetActive(false);
        
    //    manager.SetUpDialogue(dScriptPath);

    //    DialoguePanelManager dpm = dialoguePanel.GetComponent<DialoguePanelManager>();
    //    dpm.currentScript = DialogueManager.dScript;

    //    dialoguePanel.SetActive(true);
    //    dpm.Setup(0);
    //}
}
