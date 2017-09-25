using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : MonoBehaviour {

    //Queue of attacks
    public Queue<Turns> actions = new Queue<Turns>();
    //List if the player ships.
    public List<GameObject> player = new List<GameObject>();
    //List of enemys.
    public List<GameObject> enemy = new List<GameObject>();
    // reference to UI script
    public GameObject UIScripts; // to get a non-static reference to the ViewScript Object
    private ViewScript UI;

    public bool combatOver;
    public bool playerWon;

    // Use this for initialization
    void Start () {
        // to get a non-static reference to the ViewScript Object
        UI = (ViewScript)UIScripts.GetComponent("ViewScript");
        //Find and adds objects with the tag "Enemy" and "Player"
        enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        player.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        // establish that combat is NOT over, and that the player has NOT won
        combatOver = false;
        playerWon = false;

    }

	// Update is called once per frame
	void Update () {

        //Judge if the next attack is from the enemy or player. Kind of bad. Should change. 
        if (actions.Count > 0) {
            if (actions.Peek().attackerName == "FTL")
            {
                PlayerDoDamage(actions.Dequeue());
            }

            else EnemyDoDamage(actions.Dequeue());
        }

    }

    //Adds the turns into the queue.
    public void Add(Turns input) {
        actions.Enqueue(input);
    }

    //Does damage to enemy or player. Destroy target if health is reduced to 0 or less.
    void PlayerDoDamage(Turns input)
    {
        PlayerShipState test = input.attackerObject.GetComponent<PlayerShipState>();
        EnemyShipState test2 = input.targetObject.GetComponent<EnemyShipState>();
        test2.enemy.shipHealth = test2.enemy.shipHealth - test.cannon.weaponAttack;
        UI.printToCombatLog("The " + test.player.shipName + " dealt " + test.cannon.weaponAttack.ToString() + " damage to the " + test2.enemy.shipName + "!");
        if (test2.enemy.shipHealth <= 0) {
            Destroy(input.targetObject);
            enemy.Remove(input.targetObject);
            EnemyStatus.ShipHealthCurrent = 0;
            UI.printToCombatLog("The " + test.player.shipName + " has sunk the " + test2.enemy.shipName + "!");
            playerWon = true;
            combatOver = true;
        }
    }

    void EnemyDoDamage(Turns input)
    {
        EnemyShipState test = input.attackerObject.GetComponent<EnemyShipState>();
        PlayerShipState test2 = input.targetObject.GetComponent<PlayerShipState>();
        test2.player.shipHealth = test2.player.shipHealth - test.cannon.weaponAttack;
        UI.printToCombatLog("The " + test.enemy.shipName + " dealt " + test.cannon.weaponAttack.ToString() + " damage to the " + test2.player.shipName + "!");
        if (test2.player.shipHealth <= 0)
        {
            Destroy(input.targetObject);
            player.Remove(input.targetObject);
            PlayerStatus.ShipHealthCurrent = 0;
            UI.printToCombatLog("The " + test.enemy.shipName + " has sunk the " + test2.player.shipName + "!");
            playerWon = false;
            combatOver = true;
        }
    }
}
