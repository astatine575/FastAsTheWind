using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public static Inventory Inventory = new Inventory();

    public static Ship Ship;

    public static int GoldCount { get; set; }

    public static int ResourcesCount { get; set; }

    public static int AmmoCount { get; set; } // DEPRECATED: please purge from code

    public static int ShipHealthMax { get; set; } // DEPRECATED: please purge from code

    public static int ShipHealthCurrent { get; set; } // DEPRECATED: please purge from code

    public static Vector3 ShipPos { get; set; }

    public static IslandAttributes VisitingIsland { get; set; }
    public static ArrayList VisitedIslands = new ArrayList();

    public static PlayerController PlayerControllerRef { get; set; }

    public static bool Beginning = false;
}
