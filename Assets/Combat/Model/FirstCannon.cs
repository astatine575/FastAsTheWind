﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCannon : MonoBehaviour
{

    public string WeaponName {get; set;}
    public float WeaponCooldown {get; set;}
    public float CurrentCooldown {get; set;}
    public int WeaponAttack {get; set;}

    private CombatState CSM;
    private GameObject self;

    public enum States      //The states the weapon can be in.
     {
       PROCESSING,
       WAITING,
       SELECTING,
       QUEUE,
       ACTION,
       DEAD
     }

    public States CurrentState;

    protected virtual void Start () {
        CurrentState = States.PROCESSING;
        CSM = GameObject.Find("Combat Manager").GetComponent<CombatState>();
        self = this.gameObject;
    }
	
	protected virtual void Update () {

        switch (CurrentState)   //Switch between the states. 
        {
            case States.PROCESSING:
                AdvanceCooldown();
                break;
            case States.WAITING:
                break;
            case States.SELECTING:
                if (CSM.enemy.Count < 0)
                {
                    CurrentState = States.DEAD;
                }
                break;
            case States.QUEUE:
                break;
            case States.ACTION:
                break;
            case States.DEAD:
                break;
        }
    }

    void AdvanceCooldown()  //Advance the cooldown of the weapon until it reached WeaponCooldown. Switch states when it is finished.
    {
        CurrentCooldown = CurrentCooldown + Time.deltaTime;
        if (CurrentCooldown >= WeaponCooldown)
        {
            CurrentState = States.WAITING;
        }
    }

   virtual public void Fire()   //Ready the weapon for fireing.
    {
        if (CurrentState == States.WAITING) {
            CurrentState = States.SELECTING;
        }
    }

    virtual public void Target(GameObject target)   //Selects the target for fireing.
    {
        if (CurrentState == States.SELECTING) {
            CurrentState = States.QUEUE;
            AttackTarget(target);
        }
    }

    void AttackTarget(GameObject target)    //Create a new "Turn". Assign attacker,target and weapon used. Add to the attack queue.
    {
        Turns basicAttack = new Turns
        {
            attackerObject = self,
            targetObject = target,
            weaponUsed = this
        };

        CSM.Add(basicAttack);
    }

    virtual public void Reset() //Resets the weapon to 0 cooldown.
    {
        CurrentCooldown = 0;
        CurrentState = States.PROCESSING;
    }

    //tells player ship to make an attack. returns true if attack is made, false if on cooldown 
    virtual public bool CanFire() // returns true if allowed to attack
    {
        return (CurrentState == States.WAITING && PlayerStatus.AmmoCount > 0);
    }
}
