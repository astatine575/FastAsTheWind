﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCannon : FirstCannon {

    public BigCannon(){
        WeaponName = "Big Cannon";
        WeaponCooldown = 1f;
        CurrentCooldown = 0f;
        BaseAttack = 20;
        HullModifier = 1f;
        SailModifier = .5f;
        CrewModifier = 0f;
        HitRate = .75f;
        NumOfTargets = 1;
        AutoTarget = false;

    }

    public override void DoDamage(Turns target)
    {
        float hitmiss = Random.value;
        if (HitRate > hitmiss)
        {

            for (int i = 0; i < target.TargetOrder.Count; i++)
            {


                if (target.TargetOrder[i] == "HULL")
                {
                    int damage = (int)((float)BaseAttack * HullModifier);
                    target.targetObject.GetComponent<Health>().ShipHull = target.targetObject.GetComponent<Health>().ShipHull - damage;
                    CSM.TextOut(target, damage);
                }

                if (target.TargetOrder[i] == "SAIL")
                {
                    int damage = (int)((float)BaseAttack * SailModifier);
                    target.targetObject.GetComponent<Health>().ShipSail = target.targetObject.GetComponent<Health>().ShipSail - damage;
                    CSM.TextOut(target, damage);
                }

                if (target.TargetOrder[i] == "CREW")
                {
                    int damage = (int)((float)BaseAttack * CrewModifier);
                    target.targetObject.GetComponent<Health>().ShipCrew = target.targetObject.GetComponent<Health>().ShipCrew - damage;
                    CSM.TextOut(target, damage);
                }

            }

            Reset();

        }

        else {
            CSM.TextMiss(target);
            Reset();
        }



    }
}