using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private CombatState CSM;
    private GameObject self;
    private FirstCannon[] weapons;
    public List<string> orderTargets;


    void Start () {
        CSM = GameObject.Find("Combat Manager").GetComponent<CombatState>();
        self = this.gameObject;                             //Find the GameObject this is attached to.
        weapons = self.GetComponents<FirstCannon>();        //Get a list of weapons the GameObject has.
        
    }
	
	void Update () {
        foreach (FirstCannon weapons in weapons) {      //Check each weapon if the weapon can fire. If able, fire at player.
            if (weapons.CanFire()) {
                weapons.SelectWeapon();
                orderTargets.Add("HULL");
                weapons.Target(CSM.player[0], orderTargets);
                weapons.Reset();
            }
        }
	}
}
