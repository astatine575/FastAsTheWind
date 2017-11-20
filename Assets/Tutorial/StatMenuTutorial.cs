using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMenuTutorial : MonoBehaviour
{
    public Text tutorText;

    public GameObject player;

    private bool doneSpeaking;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MenuTutorial());
	}
	
	private IEnumerator MenuTutorial()
    {
        tutorText.text = "\"There's more to captaining a ship than knowing port from starboard. " +
            "When you're in charge of a ship, you're also in charge of everyone and everything on her.\" (press Enter to" +
            "continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"This here menu will let you check up on all that, and make whatever changes you see fit." +
            " Now, just hit that button that says 'Return to Map' down at the bottom.\"";

        doneSpeaking = true;
    }

    public void ReturnToMap()
    {
        if (doneSpeaking)
        {
            gameObject.SetActive(false);
            player.GetComponent<TutorialController>().SetMoveLock(false);
            player.GetComponent<TutorialController>().AdvanceTutorial();
        }
    }
}
