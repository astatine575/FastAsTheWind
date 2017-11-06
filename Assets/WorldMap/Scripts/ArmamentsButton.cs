using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentsButton : MonoBehaviour {

    public Button thisButton;
    public Text armamentName;
    public Text armamentPosition;

    public GameObject thisPanel;
    public GameObject detailsPanel;

    private ShipWeapon refWeapon;

    // Use this for initialization
    void Start()
    {
        thisButton.onClick.AddListener(OnClickDetails);
    }

    //public void SetVars(ShipWeapon item)
    //{
    //    refWeapon = item;
    //    armamentName.text = item.GetName();
    //    if (item.GetStackable()) armamentQuantity.text = item.quantity;
    //    else armamentQuantity.text = "";
    //}

    private void OnClickDetails()
    {
        thisPanel.SetActive(false);

        ItemDetailsManager detailsScript = detailsPanel.gameObject.GetComponent<ItemDetailsManager>();
        detailsScript.gameItem = refWeapon;
        detailsPanel.SetActive(true);
        //Once inventory is implemented, find a way to pass the button's stored object to the details panel
    }
}
