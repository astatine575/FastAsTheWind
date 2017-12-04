using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipState : MonoBehaviour
{
    //Gets the ship stats.
    public BasicShip player;

    void Start()
    {
        // determine player ship health from world map
        player.shipHealth = PlayerStatus.ShipHealthCurrent;
    }

    void Update()
    {
        PlayerStatus.ShipHealthCurrent = (int)player.shipHealth;

    }


}
