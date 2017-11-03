using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanels : MonoBehaviour
{
    public Button thisButton;

    public GameObject thisPanel;
    public GameObject purchaseResourcesPanel;


	void Start ()
    {
        thisButton.onClick.AddListener(OnClickSwitch);
	}
	
	private void OnClickSwitch()
    {
        thisPanel.gameObject.SetActive(false);
        purchaseResourcesPanel.gameObject.SetActive(true);
    }

}
