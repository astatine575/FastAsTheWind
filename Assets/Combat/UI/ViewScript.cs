using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScript : MonoBehaviour {

    public Health PlayerHealth;
    public Health EnemyHealth;

    public Text textPlayerShip;
    public Text textPlayerHealth;
    public Text textPlayerSail;
    public Text textPlayerCrew;

    public Text textEnemyShip;
    public Text textEnemyHealth;
    public Text textEnemySail;
    public Text textEnemyCrew;

    public Slider FTLHealthSlider;
    public Slider FTLSailSlider;
    public Slider FTLCrewSlider;

    public Slider EnemyHealthSlider;
    public Slider EnemySailSlider;
    public Slider EnemyCrewSlider;

    public Color goodHealthColor = Color.green;
    public Color averageHealthColor = Color.yellow;
    public Color badHealthColor = Color.red;
    public Text textPlayerAmmo;
    
    public Text textCombatLog; // points to Combat/Canvas/TextCombatLog
    private string[] combatLogBuffer; // a buffer to track what's displayed in the combat log.
    private int combatLogSize; // how many lines can fit into the combat log

    // Use this for initialization
    void Start () {

        PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
        EnemyHealth = GameObject.Find("Enemy").GetComponent<Health>();

        RefreshPlayerStatDisplay();
        RefreshEnemyStatDisplay();

        FTLHealthSlider = GameObject.Find("FTLHealthSlider").GetComponent<Slider>();
        EnemyHealthSlider = GameObject.Find("EnemyHealthSlider").GetComponent<Slider>();


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

    void RefreshPlayerStatDisplay()
    {
        textPlayerShip.text = "Allied Ship: FTL";
        textPlayerHealth.text = "Hull:" + PlayerHealth.ShipHull.ToString();
        textPlayerSail.text = "Sail:" + PlayerHealth.ShipSail.ToString();
        textPlayerCrew.text = "Crew:" + PlayerHealth.ShipCrew.ToString();
        textPlayerAmmo.text = "Ammo: " + PlayerStatus.AmmoCount.ToString();
        FTLHealthSlider.value = PlayerHealth.ShipHull;
        FTLSailSlider.value = PlayerHealth.ShipSail;
        FTLCrewSlider.value = PlayerHealth.ShipCrew;
    }

    void RefreshEnemyStatDisplay()
    {
        textEnemyShip.text = "Enemy Ship: Boat";
        textEnemyHealth.text = "Hull:" + EnemyHealth.ShipHull.ToString();
        textEnemySail.text = "Sail:" + EnemyHealth.ShipSail.ToString();
        textEnemyCrew.text = "Crew:" + EnemyHealth.ShipCrew.ToString();
        EnemyHealthSlider.value = EnemyHealth.ShipHull;
        EnemySailSlider.value = EnemyHealth.ShipSail;
        EnemyCrewSlider.value = EnemyHealth.ShipCrew;

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
