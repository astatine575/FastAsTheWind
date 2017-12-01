﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandProximityRadiusOne : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("island"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().ChangeHostility(-1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("island"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().ChangeHostility(1);
        }
    }
}
