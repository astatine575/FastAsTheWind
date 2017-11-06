using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathTrigger : MonoBehaviour {
    public float waitTime;
    public float fadeSpeed;
    public Text eventText;

    private void OnTriggerExit2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneIndexes.DeathScene());
    }

    private void Start()
    {
        eventText.text = "";
    }
}