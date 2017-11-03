using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsManager : MonoBehaviour
{
    public Text detailsText;
    public GameItem gameItem;

	void OnEnable()
    {
        detailsText.text = gameItem.GetAttributes();
    }
}
