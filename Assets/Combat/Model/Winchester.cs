using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winchester : FirstCannon{
	
	public Winchester(){
		WeaponName = "Winchester";
		WeaponCooldown = 3f;
		CurrentCooldown = 0f;
		WeaponAttack = 30;
	}
}
