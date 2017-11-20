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
        thisPanel.GetComponent<StatMenuTutorial>().ReturnToMap();
        thisPanel.GetComponent<SaveMenuTutorial>().ReturnToMap();
    }
}
