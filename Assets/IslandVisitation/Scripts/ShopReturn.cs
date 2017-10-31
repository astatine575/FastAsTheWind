using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopReturn : MonoBehaviour
{
    public GameObject thisPanel;
    public Button thisButton;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickReturn);
	}
	
	private void OnClickReturn()
    {
        ShopPanelManager panelScript = thisPanel.GetComponent<ShopPanelManager>();

        thisPanel.SetActive(false);
        panelScript.returnPanel.SetActive(true);
    }
}
