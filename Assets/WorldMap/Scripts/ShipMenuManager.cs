using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenuManager : MonoBehaviour
{
    public Text healthDisplay;
    public Text ammunitionsDisplay;

    public GameObject armamentsPanel;

    private void OnEnable()
    {
        healthDisplay.text = PlayerStatus.ShipHealthCurrent + "/" + PlayerStatus.ShipHealthMax;
        ammunitionsDisplay.text = PlayerStatus.AmmoCount.ToString();
    }

    private void OnDisable()
    {
        foreach(Transform child in armamentsPanel.transform)
        {
            if (child.CompareTag("ToDestroy")) GameObject.Destroy(child.gameObject);
        }
    }
}
