using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanels : MonoBehaviour
{
    public Button thisButton;

    public GameObject thisPanel;
    public GameObject destinationPanel;


	void Start ()
    {
        thisButton.onClick.AddListener(OnClickSwitch);
	}
	
	private void OnClickSwitch()
    {
        ShopPanelManager panelScript = destinationPanel.GetComponent<ShopPanelManager>();
        if(panelScript != null)
        {
            panelScript.returnPanel = thisPanel;
            if (thisPanel.CompareTag("Market")) panelScript.inventory = PlayerStatus.VisitingIsland.shopInventory;
            else if (thisPanel.CompareTag("Gunsmith")) panelScript.inventory = PlayerStatus.VisitingIsland.gunsmithInventory;
            else return;
        }
        

        thisPanel.gameObject.SetActive(false);
        destinationPanel.gameObject.SetActive(true);
    }

}
