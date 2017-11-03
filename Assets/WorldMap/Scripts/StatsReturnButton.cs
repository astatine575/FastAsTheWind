using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsReturnButton : MonoBehaviour {

    public Button thisButton;

    public GameObject statsPanel;

	void Start ()
    {
        thisButton.onClick.AddListener(OnClickReturn);
	}

    private void OnClickReturn()
    {
        PlayerStatus.PlayerControllerRef.SetMoveLock(false);
        statsPanel.SetActive(false);
    }
}
