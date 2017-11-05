using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHazard : MonoBehaviour {
	public bool collided;

	// Use this for initialization
	void Start () {
		collided = false;
		ship = GetComponent<Ship> ();
	}

	public void OnTriggerEnter2D (Collider2D collision) {
		collided = true;
	}

	public void OnTriggerExit2D (Collider2D collision) {
		collided = false;
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		while (collided) {
			ship._hullhealth = ship._hullhealth - 5;
		}
		// if the player collided with this object raduis, remove a certain amount of
		// health each update until they exit the radius
	}
}
