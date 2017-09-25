using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueButtons : MonoBehaviour {

    public Button thisButton;
    public Text buttonText;
    public DialoguePanelManager manager;

    private int nextSet;
    private int indicatedAction;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClick);
	}
	
	public void SetVars(string text, int nSet, int action)
    {
        buttonText.text = text;
        nextSet = nSet;
        indicatedAction = action;
    }

    private void OnClick()
    {
        DialogueEventHandler.CallMethods(indicatedAction);
        manager.Setup(nextSet--);
    }
}
