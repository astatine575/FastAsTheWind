using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropButton : MonoBehaviour
{
    public Button thisButton;
    public GameObject thisPanel;
    public GameObject returnPanel;

    void Start()
    {
        thisButton.onClick.AddListener(OnClickDrop);
    }

    private void OnClickDrop()
    {
        ItemDetailsManager detailsScript = thisPanel.gameObject.GetComponent<ItemDetailsManager>();
        detailsScript.gameItem.Drop();

        thisPanel.SetActive(false);
        returnPanel.SetActive(true);
    }
}
