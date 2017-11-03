using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingArrows:FirstCannon{

	public RainingArrows(){
		WeaponName = "Raining Arrows";
		WeaponCooldown = 3f;
		CurrentCooldown = 0f;
		WeaponAttack = 20;
	}
}
