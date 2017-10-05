using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PlayerStatus.VisitingIsland.SetActivePanel(gameObject);
	}

    private void OnEnable()
    {
        PlayerStatus.VisitingIsland.SetActivePanel(gameObject);
    }
}
