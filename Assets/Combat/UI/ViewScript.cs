using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScript : MonoBehaviour {

    public Health PlayerHealth;
    public Health EnemyHealth;

    public Text textPlayerShip;

    public Text textEnemyShip;

    public Slider PlayerHullSlider;
    public Slider PlayerSailSlider;
    public Slider PlayerCrewSlider;

    private Image PlayerHullFill;
    private Image PlayerHullBackground;
    private Image PlayerSailFill;
    private Image PlayerSailBackground;
    private Image PlayerCrewFill;
    private Image PlayerCrewBackground;

    public Slider EnemyHullSlider;
    public Slider EnemySailSlider;
    public Slider EnemyCrewSlider;

    private Image EnemyHullFill;
    private Image EnemyHullBackground;
    private Image EnemySailFill;
    private Image EnemySailBackground;
    private Image EnemyCrewFill;
    private Image EnemyCrewBackground;

    public Color goodHealthColor;
    public Color goodHealthColorBackground;
    public Color averageHealthColor;
    public Color averageHealthColorBackground;
    public Color badHealthColor;
    public Color badHealthColorBackground;

    public Text textPlayerAmmo;
    
    public Text textCombatLog; // points to Combat/Canvas/TextCombatLog
    private string[] combatLogBuffer; // a buffer to track what's displayed in the combat log.
    private int combatLogSize; // how many lines can fit into the combat log

    // Use this for initialization
    void Start () {

        PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
        EnemyHealth = GameObject.Find("Enemy").GetComponent<Health>();

        PlayerHullSlider.maxValue = PlayerStatus.Ship.HullHealthMax();
        PlayerSailSlider.maxValue = PlayerStatus.Ship.SailHealthMax();
        PlayerCrewSlider.maxValue = PlayerStatus.Ship.CrewHealthMax();

        PlayerHullFill = PlayerHullSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        PlayerHullBackground = PlayerHullSlider.transform.Find("Background").GetComponent<Image>();
        PlayerSailFill = PlayerSailSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        PlayerSailBackground = PlayerSailSlider.transform.Find("Background").GetComponent<Image>();
        PlayerCrewFill = PlayerCrewSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        PlayerCrewBackground = PlayerCrewSlider.transform.Find("Background").GetComponent<Image>();
        //GetComponent<Image>().color = goodHealthColor;

        EnemyHullSlider.maxValue = EnemyStatus.ShipHealthMax;
        EnemySailSlider.maxValue = 100;
        EnemyCrewSlider.maxValue = 10;

        EnemyHullFill = EnemyHullSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        EnemyHullBackground = EnemyHullSlider.transform.Find("Background").GetComponent<Image>();
        EnemySailFill = EnemySailSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        EnemySailBackground = EnemySailSlider.transform.Find("Background").GetComponent<Image>();
        EnemyCrewFill = EnemyCrewSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        EnemyCrewBackground = EnemyCrewSlider.transform.Find("Background").GetComponent<Image>();

        RefreshPlayerStatDisplay();
        RefreshEnemyStatDisplay();

        textCombatLog.text = "";
        combatLogSize = 8;
        combatLogBuffer = new string[combatLogSize];

    }
	
	// Update is called once per frame
	void Update () {
        RefreshPlayerStatDisplay();
        RefreshEnemyStatDisplay();

    }

    void RefreshPlayerStatDisplay()
    {
        textPlayerShip.text = "Player Ship: FTL";

        PlayerHullSlider.value = PlayerHealth.ShipHull;
        if (PlayerHullSlider.value / PlayerHullSlider.maxValue > .75)
        {
            PlayerHullFill.color = goodHealthColor;
            PlayerHullBackground.color = goodHealthColorBackground;
        }
        else if (PlayerHullSlider.value / PlayerHullSlider.maxValue > .25)
        {
            PlayerHullFill.color = averageHealthColor;
            PlayerHullBackground.color = averageHealthColorBackground;
        }
        else if (PlayerHullSlider.value > 0)
        {
            PlayerHullFill.color = badHealthColor;
            PlayerHullBackground.color = badHealthColorBackground;
        }
        else
        {
            PlayerHullFill.color = Color.clear;
            PlayerHullBackground.color = badHealthColorBackground;
        }

        PlayerSailSlider.value = PlayerHealth.ShipSail;
        if (PlayerSailSlider.value / PlayerSailSlider.maxValue > .75)
        {
            PlayerSailFill.color = goodHealthColor;
            PlayerSailBackground.color = goodHealthColorBackground;
        }
        else if (PlayerSailSlider.value / PlayerSailSlider.maxValue > .25)
        {
            PlayerSailFill.color = averageHealthColor;
            PlayerSailBackground.color = averageHealthColorBackground;
        }
        else if (PlayerSailSlider.value > 0)
        {
            PlayerSailFill.color = badHealthColor;
            PlayerSailBackground.color = badHealthColorBackground;
        }
        else
        {
            PlayerSailFill.color = Color.clear;
            PlayerSailBackground.color = badHealthColorBackground;
        }

        PlayerCrewSlider.value = PlayerHealth.ShipCrew;
        if (PlayerCrewSlider.value / PlayerCrewSlider.maxValue > .75)
        {
            PlayerCrewFill.color = goodHealthColor;
            PlayerCrewBackground.color = goodHealthColorBackground;
        }
        else if (PlayerCrewSlider.value / PlayerCrewSlider.maxValue > .25)
        {
            PlayerCrewFill.color = averageHealthColor;
            PlayerCrewBackground.color = averageHealthColorBackground;
        }
        else if (PlayerCrewSlider.value > 0)
        {
            PlayerCrewFill.color = badHealthColor;
            PlayerCrewBackground.color = badHealthColorBackground;
        }
        else
        {
            PlayerCrewFill.color = Color.clear;
            PlayerCrewBackground.color = badHealthColorBackground;
        }

        textPlayerAmmo.text = "Ammo: " + PlayerStatus.AmmoCount.ToString();
    }

    void RefreshEnemyStatDisplay()
    {
        textEnemyShip.text = "Enemy Ship: Boat";

        EnemyHullSlider.value = EnemyHealth.ShipHull;
        if (EnemyHullSlider.value / EnemyHullSlider.maxValue > .75)
        {
            EnemyHullFill.color = goodHealthColor;
            EnemyHullBackground.color = goodHealthColorBackground;
        }
        else if (EnemyHullSlider.value / EnemyHullSlider.maxValue > .25)
        {
            EnemyHullFill.color = averageHealthColor;
            EnemyHullBackground.color = averageHealthColorBackground;
        }
        else if (EnemyHullSlider.value > 0)
        {
            EnemyHullFill.color = badHealthColor;
            EnemyHullBackground.color = badHealthColorBackground;
        }
        else
        {
            EnemyHullFill.color = Color.clear;
            EnemyHullBackground.color = badHealthColorBackground;
        }

        EnemySailSlider.value = EnemyHealth.ShipSail;
        if (EnemySailSlider.value / EnemySailSlider.maxValue > .75)
        {
            EnemySailFill.color = goodHealthColor;
            EnemySailBackground.color = goodHealthColorBackground;
        }
        else if (EnemySailSlider.value / EnemySailSlider.maxValue > .25)
        {
            EnemySailFill.color = averageHealthColor;
            EnemySailBackground.color = averageHealthColorBackground;
        }
        else if (EnemySailSlider.value > 0)
        {
            EnemySailFill.color = badHealthColor;
            EnemySailBackground.color = badHealthColorBackground;
        }
        else
        {
            EnemySailFill.color = Color.clear;
            EnemySailBackground.color = badHealthColorBackground;
        }

        EnemyCrewSlider.value = EnemyHealth.ShipCrew;
        if (EnemyCrewSlider.value / EnemyCrewSlider.maxValue > .75)
        {
            EnemyCrewFill.color = goodHealthColor;
            EnemyCrewBackground.color = goodHealthColorBackground;
        }
        else if (EnemyCrewSlider.value / EnemyCrewSlider.maxValue > .25)
        {
            EnemyCrewFill.color = averageHealthColor;
            EnemyCrewBackground.color = averageHealthColorBackground;
        }
        else if (EnemySailSlider.value > 0)
        {
            EnemyCrewFill.color = badHealthColor;
            EnemyCrewBackground.color = badHealthColorBackground;
        }
        else
        {
            EnemyCrewFill.color = Color.clear;
            EnemyCrewBackground.color = badHealthColorBackground;
        }

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
