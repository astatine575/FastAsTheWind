using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickSetSail : MonoBehaviour
{

    public void LoadWorldMap()
    {
        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }
}
