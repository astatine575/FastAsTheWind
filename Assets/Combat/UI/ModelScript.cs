using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour {

    public GameObject combatScripts; // to get a non-static reference to the ViewScript Object
    private ViewScript viewScript; // to get a non-static reference to the ViewScript Object

    // Use this for initialization
    void Start () {
        viewScript = (ViewScript)combatScripts.GetComponent("ViewScript"); // to get a non-static reference to the ViewScript Object
        PlayerStatus.AmmoCount = 10; // DEBUG CODE REMOVE IN MVP
        PlayerStatus.ShipHealthMax = 100; // DEBUG CODE REMOVE IN MVP
        PlayerStatus.ShipHealthCurrent = 100; // DEBUG CODE REMOVE IN MVP

        EnemyStatus.ShipHealthMax = 50;
        EnemyStatus.ShipHealthCurrent = 50;
        EnemyStatus.AmmoCount = 10;
    }
	
	// Update is called once per frame
	void Update () {
		// TODO: Enemy ship firing and cooldown tracking
	}

    public void Fire() {
        // TODO: Add parameter "weapon" and weapons
        int weaponDamage = 10;
        EnemyStatus.ShipHealthCurrent -= weaponDamage;
        PlayerStatus.AmmoCount--;
        viewScript.printToCombatLog("We hit them for " + weaponDamage.ToString() + " damage!");
        viewScript.printToCombatLog("They have " + EnemyStatus.ShipHealthCurrent.ToString() + " health left!");
    }
}
