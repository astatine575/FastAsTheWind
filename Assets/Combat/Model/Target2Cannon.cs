using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2Cannon : FirstCannon {

    public Target2Cannon() {
        WeaponName = "2 Shot";
        WeaponCooldown = 5f;
        CurrentCooldown = 0f;
        BaseAttack = 40;
        HullModifier = .8f;
        SailModifier = .8f;
        CrewModifier = 0f;
        HitRate = .8f;
        NumOfTargets = 2;
        AutoTarget = true;
    }

    public override void DoDamage(Turns target)
    {
        target.targetObject.GetComponent<Health>().ShipHull -= BaseAttack;
        target.targetObject.GetComponent<Health>().ShipSail -= BaseAttack;
        Reset();
    }

}
