using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipState : MonoBehaviour
{
    //Gets the ship stats.
    public BasicShip player;

    //Gets the weapon stats.
    public BaseWeapon cannon;

    //For list of player ships and enemy ships and action queue.
    private CombatState CSM;

    //States that the weapon could be in. Should be part of BaseWeapon.
    public enum States
    {
        PROCESSING,
        QUEUE,
        SELECTING,
        WAITING,
        ACTION,
        DEAD
    }

    //The current state the weapon is in.
    public States currentState;

    void Start()
    {
        // determine player ship health from world map
        player.shipHealth = PlayerStatus.ShipHealthCurrent;
        // First state where the weapon will count up until it can be used. Subject to change of course.
        currentState = States.PROCESSING;

        CSM = GameObject.Find("Combat Manager").GetComponent<CombatState>();
    }

    void Update()
    {
        PlayerStatus.ShipHealthCurrent = (int)player.shipHealth;
        //Debug.Log(currentState);

        //Switch case for the weapon states.
        //As long as the weapon is in processing, the current cooldown will count up.
        //When it reaches the queue state, it will create a action or turn and add it to the queue.

        switch (currentState)
        {
            case States.PROCESSING:
                advanceCooldown();
                break;
            case States.QUEUE:
                if(CSM.enemy.Count == 0)
                    currentState = States.DEAD;
                break;
            case States.SELECTING:
                break;
            case States.WAITING:
                break;
            case States.ACTION:
                break;
            case States.DEAD:
                break;
        }

    }

    //Will count up untill it reaches the weapon cooldown time. When it reaches the cooldown time it will change states.
    void advanceCooldown()
    {
        cannon.currentCooldown = cannon.currentCooldown + Time.deltaTime;
        if (cannon.currentCooldown >= cannon.weaponCooldown)
        {
            currentState = States.QUEUE;
        }
    }

    //tells player ship to make an attack. returns true if attack is made, false if on cooldown 

    public void attackTarget() {
        //Create a new "Turn"
        //Gets attacker Objets and target Objects from Combat State CSM.    
        Turns basicAttack = new Turns();
        basicAttack.attackerName = player.shipName;
        basicAttack.attackerObject = CSM.player[0];
        basicAttack.targetObject = CSM.enemy[0];
        //Add to the attack queue.
        CSM.Add(basicAttack);
        //Reset cooldown and state/
        cannon.currentCooldown = 0f;
        currentState = States.PROCESSING;
    }

    public bool canFire() // returns true if allowed to attack
    {
        return (currentState == States.QUEUE);
    }

}
