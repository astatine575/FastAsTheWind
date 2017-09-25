using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventHandler : MonoBehaviour {

	public static void CallMethods(int methodToCall)
    {
        if (methodToCall == 0) DamageShip();
    }

    public static void DamageShip() //0
    {
        PlayerStatus.ShipHealthCurrent = PlayerStatus.ShipHealthCurrent / 2;
    }
}
