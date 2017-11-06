using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetTrigger : MonoBehaviour
{
    public float waitTime;
    public float fadeSpeed;
    public Text eventText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
                PlayerStatus.TriggerStatus = false;
    }



    private void Start()
    {
        eventText.text = "";
    }
}
