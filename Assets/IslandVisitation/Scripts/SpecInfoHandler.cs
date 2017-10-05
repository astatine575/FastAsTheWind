using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecInfoHandler : MonoBehaviour
{
    public Button gatherButton;
    public Text gatherButtonText;


	// Use this for initialization
	void OnEnable ()
    {
        string text = PlayerStatus.VisitingIsland.GetSpecInfoButtonText();
		if((text != "NONE"))
        {
            gatherButton.transform.gameObject.SetActive(true);
            gatherButtonText.text = text;
        }
        else gatherButton.transform.gameObject.SetActive(false);
    }

}
