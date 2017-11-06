using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

    public Button thisButton;
    public GameObject thisPanel;
    public GameObject targetPanel;

    void Start()
    {
        thisButton.onClick.AddListener(OnClickEquip);
    }

    private void OnClickEquip()
    {
        ItemDetailsManager detailsScript = thisPanel.gameObject.GetComponent<ItemDetailsManager>();
        detailsScript.Equip();
    }
}
