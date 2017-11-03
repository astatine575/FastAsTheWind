using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour {

    public Button thisButton;
    public GameObject panelRef;

    private GameItemPurchaseManager panelScript;

    // Use this for initialization
    void Start()
    {
        thisButton.onClick.AddListener(OnClickCall);

        panelScript = panelRef.gameObject.GetComponent<GameItemPurchaseManager>();
    }

    private void OnClickCall()
    {
        if (panelScript == null) panelRef.gameObject.GetComponent<ResourcesPurchaseManager>().Purchase();
        else panelScript.Purchase();
    }
}
