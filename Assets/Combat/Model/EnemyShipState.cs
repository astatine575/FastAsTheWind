using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipState : MonoBehaviour {

    //The same as Player Ship State, just for the enemy ship.

    public BasicShip enemy;

    public BaseWeapon cannon = new BaseWeapon();

    private CombatState CSM;

    public enum States
    {
        PROCESSING,
        QUEUE,
        SELECTING,
        WAITING,
        ACTION,
        DEAD
    }

    public States currentState;

    void Start()
    {
        // determine enemy ship health from world map
        enemy.shipHealth = EnemyStatus.ShipHealthCurrent;
        currentState = States.PROCESSING;
        CSM = GameObject.Find("Combat Manager").GetComponent<CombatState>();

    }

    void Update()
    {
        EnemyStatus.ShipHealthCurrent = (int)enemy.shipHealth;

        //Debug.Log(currentState);

        switch (currentState)
        {
            case States.PROCESSING:
                advanceCooldown();
                break;
            case States.QUEUE:
                if (CSM.player.Count > 0)
                    attackTarget();
                else {
                    currentState = States.DEAD;
                }
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

    void advanceCooldown()
    {
        cannon.currentCooldown = cannon.currentCooldown + Time.deltaTime;
        if (cannon.currentCooldown >= cannon.weaponCooldown)
        {
            currentState = States.QUEUE;
        }
    }

    void attackTarget()
    {
        Turns basicAttack = new Turns();
        basicAttack.attackerName = enemy.shipName;
        basicAttack.attackerObject = CSM.enemy[0];
        basicAttack.targetObject = CSM.player[0];
        CSM.Add(basicAttack);
        cannon.currentCooldown = 0f;
        currentState = States.PROCESSING;

    }
}
