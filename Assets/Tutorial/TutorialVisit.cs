using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialVisit : MonoBehaviour
{
    public Text islandID;
    public Text enterPrompt;
    public Text tutorText;

    private TutorialController controller;

    private void Start()
    {
        controller = transform.parent.GetComponent<TutorialController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("island"))
        {
            IslandAttributes island = (IslandAttributes)collision.gameObject.GetComponent(typeof(IslandAttributes));
            islandID.text = "Home";

            if(controller.GetTutorStatus() == "move" || controller.GetTutorStatus() == "visit") controller.SetVisiting(true);

            if (controller.GetTutorStatus() == "visit") tutorText.text = "\"When you get close enough to an island, you can dock " +
                    "at that island by pressing enter. Why don't you try that now?\"";

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
