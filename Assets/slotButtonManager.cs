using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotButtonManager : MonoBehaviour {
    public Button slotButton;
    private weaponinventory inven;
    public Text slotButton_uppertext;
    public Text slotButton_lowertext;

    void Start(){
        slotButton.onClick.AddListener(OnClick);
        slotButton_uppertext.text = "";
        slotButton_lowertext.text = "";
    }

    public void OnClick(){
        
    }
}
