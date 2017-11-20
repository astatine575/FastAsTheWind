using UnityEngine;
using System.Collections.Generic;

public class IslandGeneration : MonoBehaviour{

    private List<Vector3> islandLocations;
    private List<Vector3> islandOffsets;
    public int numberOfIslands;
    public float minDistanceBtwIslands;
    public float closestDistanceToSpawn;
    public float furthestDistanceToSpawn;

    public List<Vector3> generateOffsets()
    {

        islandOffsets = new List<Vector3>(numberOfIslands);
        //For getting all the island offsets
        for(int i=0; i<numberOfIslands; i++)
        {
            float a = 0f, b = 0f, sign_a = 0f, sign_b = 0f;
            a = Random.Range(5f, 7f); b = Random.Range(5f, 7f);
            sign_a = Random.Range(-1f, 1f); sign_b = Random.Range(-1f, 1f);
            a = sign_a > 0 ? a : -a;
            b = sign_b > 0 ? b : -b;
            Vector3 offset = new Vector3(a, b, 0);
            islandOffsets.Add(offset);
        }

        return islandOffsets;
        
    }

    public List<Vector3> generateIslands()
    {
        islandLocations = new List<Vector3>(numberOfIslands);
        

        for (int i = 0; i<numberOfIslands; i++)
        {

            //For Getting all the island locations
            float closestIsland = 0f;
            float x = 0f, y = 0f;

            while (closestIsland < minDistanceBtwIslands)
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