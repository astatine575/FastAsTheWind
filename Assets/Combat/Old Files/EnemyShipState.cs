using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipState : MonoBehaviour
{

    //The same as Player Ship State, just for the enemy ship.

    public BasicShip enemy;


    void Start()
    {
        // determine enemy ship health from world map
        enemy.shipHealth = EnemyStatus.ShipHealthCurrent;
        
       

    }
}
