using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScript : MonoBehaviour {

    public Text textPlayerShip;
    public Slider FTLHealthSlider;
    public Slider EnemyHealthSlider;
    public Color goodHealthColor = Color.green;
    public Color averageHealthColor = Color.yellow;
    public Color badHealthColor = Color.red;
    public Text textPlayerAmmo;
    public Text textEnemyShip;
    public Text textCombatLog; // points to Combat/Canvas/TextCombatLog
    private string[] combatLogBuffer; // a buffer to track what's displayed in the combat log.
    private int combatLogSize; // how many lines can fit into the combat log

    // Use this for initialization
    void Start () {

        RefreshPlayerStatDisplay();
        RefreshEnemyStatDisplay();

        textCombatLog.text = "";
        combatLogSize = 6;
        combatLogBuffer = new string[combatLogSize];

    }
	
	// Update is called once per frame
	void Update () {
        //TODO: update the UI to match global variables
        RefreshPlayerStatDisplay();
        RefreshEnemyStatDisplay();

    }

    void RefreshPlayerStatDisplay() {
        textPlayerShip.text = "Allied Ship: FTL";
       // FTLHealthSlider.value = PlayerStatus.ShipHealthCurrent;
        textPlayerAmmo.text = "Ammo: " + PlayerStatus.AmmoCount.ToString();
    }

    void RefreshEnemyStatDisplay()
    {
        textEnemyShip.text = "Enemy Ship: Boat";
       // EnemyHealthSlider.value = EnemyStatus.ShipHealthCurrent;
    }

    public void printToCombatLog(string line)
    {
        for (int i = combatLogSize - 1; i > 0; i--) // shift the buffer up one
            combatLogBuffer[i] = combatLogBuffer[i - 1];
        combatLogBuffer[0] = line; // add the new line to the buffer
        string result = combatLogBuffer[combatLogSize - 1]; // create the new buffer string
        for (int i = combatLogSize - 2; i >= 0; i--) // add the rest of the buffer to it
            result = result + "\n" + combatLogBuffer[i];
        textCombatLog.text = result;
    }
}
