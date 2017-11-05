using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolRadius : MonoBehaviour {
	public float Whirlpoolstrength;

	private bool collided;
	private Rigidbody2D player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody2D> ();
		collided = false;
	}

	public void OnTriggerEnter2D (Collider2D collision) {
		collided = true;
	}

	public void OnTriggerExit2D (Collider2D collision) {
		collided = false;
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if (collided) {
			Vector2 forceDirection = transform.position - player.transform.position;
			forceDirection = forceDirection.normalized;
			player.AddForce (forceDirection * Whirlpoolstrength);
		}
	}
}
