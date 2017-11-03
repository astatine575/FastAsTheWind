using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class DiscoverRadius : MonoBehaviour {

    public Text eventText;

    public float waitTime;
    public float fadeSpeed;

    private void Start()
    {
        eventText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("island"))
        {
            IslandAttributes island = (IslandAttributes)collision.gameObject.GetComponent(typeof(IslandAttributes));
            if (!PlayerStatus.VisitedIslands.Contains(island.islandName))
            {
                StopCoroutine(TextFade());

                //island.SetDiscovered();
                PlayerStatus.VisitedIslands.Add(island.islandName);
                eventText.text = "Discovered " + island.islandName + "!";
                StartCoroutine(TextFade());
            }
        }
    }

    private IEnumerator TextFade()
    {
        eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 1);

        yield return new WaitForSeconds(waitTime);

        while (eventText.color.a > 0)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a - (Time.deltaTime * fadeSpeed));

            yield return null;
        }
    }
}
