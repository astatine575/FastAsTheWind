using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour {

    // Use this for initialization

    public Button loadButton; 

	void Start () {

        loadButton.onClick.AddListener(OnThisClick);
		
	}

	private void OnThisClick()
    {
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
