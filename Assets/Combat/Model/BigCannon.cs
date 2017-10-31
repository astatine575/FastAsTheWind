using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCannon : FirstCannon {

    public BigCannon(){
        WeaponName = "Big Cannon";
        WeaponCooldown = 5f;
        CurrentCooldown = 0f;
        BaseAttack = 50;
        HullModifier = 1f;
        SailModifier = .5f;
        CrewModifier = 0f;
        HitRate = .75f;
        NumOfTargets = 1;
        AutoTarget = false;

    }

    public override void DoDamage(Turns target)
    {

        for (int i = 0; i < target.TargetOrder.Count; i++) {


            if (target.TargetOrder[i] == "HULL") {
                target.targetObject.GetComponent<Health>().ShipHull -= BaseAttack;
            }

            if (target.TargetOrder[i] == "SAIL")
            {
                target.targetObject.GetComponent<Health>().ShipSail -= BaseAttack;
            }

            if (target.TargetOrder[i] == "CREW")
            {
                target.targetObject.GetComponent<Health>().ShipCrew -= BaseAttack;
            }

        }

        Reset();


    }
}
