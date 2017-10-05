using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseWeapon{

    public string weaponName;
    public float weaponCost;
    public float weaponCooldown;
    public float currentCooldown = 0f;
    public float weaponAttack;

}
