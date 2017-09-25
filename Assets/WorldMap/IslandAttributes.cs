using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandAttributes : MonoBehaviour
{

    public string islandName;
    public int islandID;
    public bool hasBlacksmith;
    public bool hasGunsmith;
    public bool hasTavern;
    public bool hasWardsmith;
    public bool hasArchetier;
    public bool hasSpecial;

    private bool isDiscovered;

    public void SetAttributes(string name, bool blackS, bool gunS, bool tav, bool wardS, bool arch, bool special)
    {
        islandName = name; hasBlacksmith = blackS; hasGunsmith = gunS; hasTavern = tav;
        hasWardsmith = wardS; hasArchetier = arch; hasSpecial = special;
    }

    public void SetDiscovered()
    {
        isDiscovered = true;
    }

    public bool GetDiscovered()
    {
        return isDiscovered;
    }

    public string GetName()
    {
        return islandName;
    }

    // Use this for initialization
    void Start ()
    {
        isDiscovered = false;
        transform.position = IslandStats.IslandLocations[islandID];
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
