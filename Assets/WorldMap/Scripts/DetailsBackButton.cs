using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsBackButton : MonoBehaviour
{
    public Button thisButton;
    public GameObject thisPanel;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickBack);
	}
	
	private void OnClickBack()
    {
        thisPanel.GetComponent<ItemDetailsManager>().Back();
    }
}
