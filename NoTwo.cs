//By yichen Oct 26
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTwo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		Active ();
		Disappear ();
	}
	public void Active(){
		if(Input.GetButtonDown("2"))
		{ 

			gameObject.SetActive(true);


		} 

	}
	public void Disappear(){
		if(Input.GetButtonDown("1"))
		{ 

			gameObject.SetActive(false);


		} 
	}
}
