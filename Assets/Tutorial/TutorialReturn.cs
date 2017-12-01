using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialReturn : MonoBehaviour
{
    public GameObject thisPanel;
    public Button thisButton;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClick);
	}

    private void OnClick()
    {
        if (thisPanel.GetComponent<StatMenuTutorial>() != null) thisPanel.GetComponent<StatMenuTutorial>().ReturnToMap();
        else thisPanel.GetComponent<SaveMenuTutorial>().ReturnToMap();
    }
}
