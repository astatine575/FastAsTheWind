using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScript : MonoBehaviour {

    public Text textPlayerShip;
    public Text textPlayerHealth;
    public Text textEnemyShip;
    public Text textEnemyHealth;
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
        textPlayerHealth.text = "HP: " + PlayerStatus.ShipHealthCurrent.ToString() + "/" + PlayerStatus.ShipHealthMax.ToString();
    }

    void RefreshEnemyStatDisplay()
    {
        textEnemyShip.text = "Enemy Ship: Boat";
        textEnemyHealth.text = "HP: " + EnemyStatus.ShipHealthCurrent.ToString() + "/" + EnemyStatus.ShipHealthMax.ToString();
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
