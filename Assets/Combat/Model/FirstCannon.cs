using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FirstCannon : MonoBehaviour
{

    public string WeaponName {get; set;}
    public float WeaponCooldown {get; set;}
    public float CurrentCooldown {get; set;}

    public int BaseAttack {get; set;}
    public float HullModifier {get; set;}
    public float SailModifier {get; set;}
    public float CrewModifier {get; set;}

    public float HitRate { get; set;}

    public int NumOfTargets {get; set;}
    public bool AutoTarget {get; set;}
    private CombatState CSM;
    private GameObject Self;

    public enum States  //The states the weapon can be in.
     {
       PROCESSING,
       WAITING,
       SELECTING,
       QUEUE,
       ACTION,
       PAUSED,
       DEAD
     }

    public States CurrentState;
    public States PausedState;

    protected virtual void Start () {
        CurrentState = States.PROCESSING;
        CSM = GameObject.Find("Combat Manager").GetComponent<CombatState>();
        Self = this.gameObject;
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

    private void AdvanceCooldown()  //Advance the cooldown of the weapon until it reached WeaponCooldown. Switch states when it is finished.
    {
        CurrentCooldown = CurrentCooldown + Time.deltaTime;
        if (CurrentCooldown >= WeaponCooldown)
        {
            CurrentState = States.WAITING;
        }
    }
    
    public bool CanFire()   //Tells if the weapon can fire, returns true if able, false otherwise.
    {
        return (CurrentState == States.WAITING && PlayerStatus.AmmoCount > 0);
    }

    public void SelectWeapon()  //Ready the weapon for fireing.
    {
        if (CurrentState == States.WAITING)
        {
            CurrentState = States.SELECTING;
        }
    }

    public void DeselectWeapon()    //If you want to switch weapons.
    {
        if (CurrentState == States.SELECTING)
        {
            CurrentState = States.WAITING;
        }
    }

    virtual public void Target(GameObject target, List<string> targetOrder) //Add the current turn to the queue.

    {
        if (CurrentState == States.SELECTING) {
            CurrentState = States.QUEUE;
            Turns basicAttack = new Turns
            {
                attackerObject = Self,
                targetObject = target,
                weaponUsed = this,
                TargetOrder = targetOrder,
            };
            CSM.Add(basicAttack);
        }
    }

    public abstract void DoDamage(Turns target);

    virtual public void Reset() //Resets the weapon to 0 cooldown.
    {
        CurrentCooldown = 0;
        CurrentState = States.PROCESSING;
    }

}
