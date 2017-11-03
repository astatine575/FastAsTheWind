using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitingRadius : MonoBehaviour {

    public Text islandID;
    public Text enterPrompt;

    private PlayerController controller;

    private void Start()
    {
        controller = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("island"))
        {
            IslandAttributes island = (IslandAttributes)collision.gameObject.GetComponent(typeof(IslandAttributes));
            islandID.text = island.GetName();

            controller.SetVisiting(true);
            PlayerStatus.VisitingIsland = island;

            enterPrompt.text = "Press Enter to dock.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("island"))
        {
            islandID.text = "";
            enterPrompt.text = "";

            controller.SetVisiting(false);
            PlayerStatus.VisitingIsland = null;
        }
    }
}
