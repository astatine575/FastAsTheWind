using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{

    public static int GoldCount { get; set; }

    public static int ResourcesCount { get; set; }

    public static int AmmoCount { get; set; }

    public static int ShipHealthMax { get; set; }

    public static int ShipHealthCurrent { get; set; }

    public static Vector3 ShipPos { get; set; }

    public static IslandAttributes VisitingIsland { get; set; }
    public static PlayerController PlayerControllerRef { get; set; }

}
