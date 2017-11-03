using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBackButton : MonoBehaviour
{
    public Button thisButton;
    public GameObject thisPanel;
    public GameObject mainPanel;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickBack);
	}

    private void OnClickBack()
    {
        thisPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
