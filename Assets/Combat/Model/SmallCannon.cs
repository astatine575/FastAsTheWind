using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCannon : FirstCannon {

    public SmallCannon(){
        WeaponName = "Small Cannon";
        WeaponCooldown = 2f;
        CurrentCooldown = 0f;
        BaseAttack = 2;
        HullModifier = 1.2f;
        SailModifier = 1.2f;
        CrewModifier = 0f;
        HitRate = .85f;
        NumOfTargets = 1;
        AutoTarget = false;
    }

    public override void DoDamage(Turns target)
    {
        if (target.Hull) {
            target.targetObject.GetComponent<Health>().ShipHull -= BaseAttack;
        }

        if (target.Sail) {
            target.targetObject.GetComponent<Health>().ShipSail -= BaseAttack;
        }

        if (target.Crew) {
            target.targetObject.GetComponent<Health>().ShipCrew -= BaseAttack;
        }

        Reset();

    }
}
