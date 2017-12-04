using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    private GameObject Self;
    private Animator Shoot;

    public bool Fire = false;

    void Start() {
        Self = this.gameObject;
        Shoot = Self.GetComponent<Animator>();
        


    }

	void Update () {
        Debug.Log(Fire);
        Shoot.SetBool("Fire", Fire);
    }
}
