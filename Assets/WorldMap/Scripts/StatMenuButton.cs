using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMenuButton : MonoBehaviour
{
    public Button thisButton;

    public GameObject parentPanel;
    public GameObject destinationPanel;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickInventory);
	}

    private void OnClickInventory()
    {
        parentPanel.SetActive(false);
        destinationPanel.SetActive(true);
    }

}
