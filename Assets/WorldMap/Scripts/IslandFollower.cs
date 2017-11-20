using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFollower : MonoBehaviour
{

    public int id;

    // Use this for initialization
    void Start()
    {

        transform.position = IslandStats.IslandLocations[id] + IslandStats.IslandOffsets[id];
    }
}
