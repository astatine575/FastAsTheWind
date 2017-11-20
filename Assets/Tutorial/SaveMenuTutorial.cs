using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuTutorial : MonoBehaviour
{
    public Text tutorText;
    public GameObject player;

    private bool doneSpeaking = false;

    void Start()
    {
        StartCoroutine(MenuTutorial());
    }

    private IEnumerator MenuTutorial()
    {
        tutorText.text = "\"Another duty you have as captain is to keep detailed records. Remember to update your records often;" +
            " if something were to happen to the ship without being recorded. . . it makes me shudder even to think it!\" " +
            "(press ENTER to continue)";

        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"Anyway, you can exit this menu the same way you did the other. " +
            "Just hit that button down at the bottom.\"";

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
