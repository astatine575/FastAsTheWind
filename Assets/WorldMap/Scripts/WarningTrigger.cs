using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningTrigger : MonoBehaviour {
    public float waitTime;
    public float fadeSpeed;
    public Text eventText;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!PlayerStatus.TriggerStatus)
            {
                PlayerStatus.TriggerStatus = true;

                eventText.text = "WARNING !!!";
                StartCoroutine(WarningFade());
            }
        }
    }

    private IEnumerator WarningFade()
    {
        eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 1);
        yield return new WaitForSeconds(waitTime);
        while (eventText.color.a > 0)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a - (Time.deltaTime * fadeSpeed));
            yield return null;
        }
    }


    private void Start()
    {
        eventText.text = "";
    }
}