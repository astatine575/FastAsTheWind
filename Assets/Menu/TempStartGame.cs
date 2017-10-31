using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempStartGame : MonoBehaviour {

	// Use this for initialization
	public void OnClickStartGame()
    {
        PlayerStatus.ResourcesCount = 100;
        PlayerStatus.GoldCount = 100;
        PlayerStatus.ShipPos = new Vector3(0, 0, 0);
        PlayerStatus.Beginning = true;
        //PlayerStatus.ShipHealthCurrent = 100;
        //PlayerStatus.AmmoCount = 20; //Remove this once AmmoCount is phased out of all code

        QuestsStatus.testQuestStatus = -1;

        IslandStats.IslandLocations = GetComponent<IslandGeneration>().generateIslands();

        SceneManager.LoadScene(SceneIndexes.WorldMap());

        PlayerStatus.Ship = new Ship(
                                          "FTL",                            // name
                                          "Your trusty old sloop. She may not be much, but she served you well during your time as a merchant.", // description
                                          1000,                             // value
                                          Ship.ShipClass.SLOOP,             // ship class
                                          100,                              // base hull health
                                          10,                               // base/necessary crew health/count
                                          50                                // base sail health
            );
        PlayerStatus.Ship._hullHealth = PlayerStatus.Ship.HullStrengthMax();
        //for (int i = 0; i < 10; i++)
        //    PlayerStatus.Inventory.AddItem(new CrewWeapon(
        //                                    "Sword",                        // name
        //                                    "A four-foot long steel blade with a leather-bound hilt. A well made sword, but nothing to brag about.", // description
        //                                    50,                             // value
        //                                    CrewWeapon.WeaponMaterial.STEEL,// weapon material
        //                                    100,                            // current condition
        //                                    100,                            // maximum condition
        //                                    2));                            // damage
        //for (int i = 0; i < 4; i++)
        //    PlayerStatus.Inventory.AddItem(new ShipWeapon(
        //                                    "Basic Cannon",   // name
        //                                    "The old and reliable 20mm naval gun, a common weapon used by merchant, pirate, and naval vessels alike.", // description
        //                                    125,                             // value
        //                                    ShipWeapon.WeaponType.CANNON,   // weapon type
        //                                    Ammunition.AmmoType.CANNONBALL, // ammo type
        //                                    2,                              // cooldown
        //                                    10));                           // damage
        //PlayerStatus.Inventory.AddItem(new Consumable(
        //                                    "Health Potion",                // name
        //                                    "A blood-red potion, used to magically heal an individual's wounds.", // description
        //                                    5,                              // value
        //                                    Consumable.Effect.HEAL,         // effect
        //                                    10),                            // magnitude
        //                                    4);                             // quantity
        PlayerStatus.Inventory.AddItem(new Ammunition(
                                            "Explosive Round",             // name
                                            "Filled to the brim with brimstone, these cannonballs violently explode on impact, greviously wounding enemy crew.", // description
                                            2,                             // value
                                            Ammunition.AmmoType.CANNONBALL, // weapon material
                                            1,                              // hull damage multiplier
                                            3,                              // crew damage multiplier
                                            1),                             // sail damage multiplier
                                            100);                           // quantity
    }
}
