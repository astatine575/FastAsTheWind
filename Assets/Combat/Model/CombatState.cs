using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : MonoBehaviour {

    public Queue<Turns> actions = new Queue<Turns>();   //Queue of attacks
    public List<GameObject> player = new List<GameObject>();     //List of the players.
    public List<GameObject> enemy = new List<GameObject>(); //List of enemies.
    public GameObject UIScripts;    // reference to UI script
    private ViewScript UI;  // to get a non-static reference to the ViewScript Object

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
    }

    public void TextOut (Turns input,int damage){

        Health targetHealth = input.targetObject.GetComponent<Health>();

        if (input.targetObject.tag == "Enemy") {
            UI.printToCombatLog("The " + "Player" + " dealt " + damage.ToString() + " damage to the " + "Enemy" + "!");
            PlayerStatus.AmmoCount -= 1;
        }

            
        else

            UI.printToCombatLog("The " + "Enemy" + " dealt " + damage.ToString() + " damage to the " + "Player" + "!");

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

    public void TextMiss(Turns input) {
        if (input.targetObject.tag == "Enemy") {
            UI.printToCombatLog("The " + "Player" + " missed the " + "Enemy" + "!");
            PlayerStatus.AmmoCount -= 1;
        }

           
        else

            UI.printToCombatLog("The " + "Enemy" + " missed the "  + "Player" + "!");

    }

}
