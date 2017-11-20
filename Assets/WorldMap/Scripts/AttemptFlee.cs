using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttemptFlee : MonoBehaviour
{
    public Button thisButton;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        thisButton.onClick.AddListener(OnClickFlee);
	}
	
	private void OnClickFlee()
    {
        //TODO: add some sort of randomness and a chance to fail
        gameObject.transform.parent.gameObject.SetActive(false);
        player.GetComponent<PlayerController>().SetMoveLock(false);
    }
}
