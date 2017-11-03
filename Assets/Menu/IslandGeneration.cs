using UnityEngine;
using System.Collections.Generic;

public class IslandGeneration : MonoBehaviour{

    private List<Vector3> islandLocations;
    public int numberOfIslands;
    public float minDistanceBtwIslands;
    public float closestDistanceToSpawn;
    public float furthestDistanceToSpawn;

    public List<Vector3> generateIslands()
    {
        islandLocations = new List<Vector3>(numberOfIslands);

        for (int i = 0; i<numberOfIslands; i++)
        {
            float closestIsland = 0f;
            float x = 0f, y = 0f;

            while (closestIsland < 10f)
            {
                x = Random.Range(-1f, 1f); y = Random.Range(-1f, 1f) > 0 ? Mathf.Sqrt(1 - x * x) : -Mathf.Sqrt(1 - x * x);
                x = Random.Range(closestDistanceToSpawn, furthestDistanceToSpawn) * x;
                y = Random.Range(closestDistanceToSpawn, furthestDistanceToSpawn) * y;
                if (i == 0) { break; }
                closestIsland = CalculateCloseIsland(x, y, i);
            }

            islandLocations.Add(new Vector3(x, y, 0));
            
        }
        return islandLocations;

    }

    //private Vector3 RandomLocation(int numbIslandGen)
    //{
    //    float closestIsland = 0f;
    //    float x = 0f, y = 0f;

    //    while (closestIsland < 10f) {
    //        x = Random.Range(-1f, 0f); y = Random.Range(-1f, 1f) > 0 ? Mathf.Sqrt(1 - x * x) : -Mathf.Sqrt(1 - x * x);
    //        x = Random.Range(15f, 18f) * x;
    //        y = Random.Range(15f, 18f) * y;
    //        Debug.Log(x + " " + y + " " + " " + numbIslandGen);
    //        if(numbIslandGen == 0) { break; }
    //        closestIsland = CalculateCloseIsland(x, y, numbIslandGen);
    //    }



    //    return new Vector3(x, y, 0);
    //}

    private float CalculateCloseIsland(float x, float y, int numbIslandsGen) //gets the distance to the closest island as a float
    {
        float result = 0f;
        for(int i = 0; i < numbIslandsGen; i++)
        {
            
            float temp = Mathf.Sqrt(Mathf.Pow(x - islandLocations[i].x, 2f) + Mathf.Pow(y - islandLocations[i].y, 2f));
            if (i == 0)
            {
                result = temp;
            }
            else
            {
                result = temp < result ? temp : result;
            }
            
        }
        return result;
    }

}