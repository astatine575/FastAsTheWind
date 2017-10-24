using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : MonoBehaviour {

    //Queue of attacks
    public Queue<Turns> actions = new Queue<Turns>();
    //List of the player ships.
    public List<GameObject> player = new List<GameObject>();
    //List of enemys.
    public List<GameObject> enemy = new List<GameObject>();
    // reference to UI script
    public GameObject UIScripts; // to get a non-static reference to the ViewScript Object
    private ViewScript UI;

    private GameObject playerObject;

    public bool combatOver;
    public bool playerWon;

    void Start() {
        // to get a non-static reference to the ViewScript Object
        UI = (ViewScript)UIScripts.GetComponent("ViewScript");
        //Find and adds objects with the tag "Enemy" and "Player"
        enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        player.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        // establish that combat is NOT over, and that the player has NOT won
        combatOver = false;
        playerWon = false;

        playerObject = player[0];

        playerObject.AddComponent<BigCannon>();

        if (PlayerStatus.HadMoreWeapon == true) {

            playerObject.AddComponent<SmallCannon>();
            playerObject.AddComponent<SmallCannon>();
            playerObject.AddComponent<SmallCannon>();
        }


    }

    void Update() {

        if (Time.timeScale == 0) { return; }

        if (actions.Count > 0) {
            DoTurn(actions.Dequeue());
        }

    }

    //Adds the turns into the queue.
    public void Add(Turns input) {
        actions.Enqueue(input);
    }

    void DoTurn(Turns input) {
        GameObject target = input.targetObject;
        FirstCannon weapon = input.weaponUsed;
        weapon.DoDamage(input);
        TextOut(input);
    }

    public void TextOut (Turns input){

        Health targetHealth = input.targetObject.GetComponent<Health>();
        if (input.targetObject.tag == "Player")
            UI.printToCombatLog("The " + "Player" + " dealt " + input.weaponUsed.BaseAttack.ToString() + " damage to the " + "Enemy" + "!");
        else
            UI.printToCombatLog("The " + "Enemy" + " dealt " + input.weaponUsed.BaseAttack.ToString() + " damage to the " + "Enemy" + "!");


        if (targetHealth.ShipHull <= 0){
            Destroy(input.targetObject);
            if (input.targetObject.tag == "Player") {
                player.Remove(input.targetObject);
                PlayerStatus.ShipHealthCurrent = 0;
                UI.printToCombatLog("The " + "Enemy" + " has sunk the " + "Player" + "!");
                playerWon = false;
                combatOver = true;
            }
            else {
                enemy.Remove(input.targetObject);
                EnemyStatus.ShipHealthCurrent = 0;
                UI.printToCombatLog("The " + "Player" + " has sunk the " + "Enemy" + "!");
                playerWon = true;
                combatOver = true;
            }
        }
    }

}
