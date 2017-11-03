using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuantityButton : MonoBehaviour
{
    public int magnitude;

    public Button thisButton;
    public GameObject panelRef;

    private GameItemPurchaseManager panelScript;

	// Use this for initialization
	void Start ()
    {
        panelScript = panelRef.gameObject.GetComponent<GameItemPurchaseManager>();
        if(panelScript == null)panelRef.gameObject.GetComponent<GameItemPurchaseManager>();
        thisButton.onClick.AddListener(OnClickCall);

        
	}

    private void OnClickCall()
    {
        if (panelScript == null) panelRef.gameObject.GetComponent<ResourcesPurchaseManager>().AlterQuantity(magnitude);
        else panelScript.AlterQuantity(magnitude);
    }
}
