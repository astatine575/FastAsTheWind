using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempStartGame : MonoBehaviour {

	// Use this for initialization
	public void OnClickStartGame()
    {
        PlayerStatus.ResourcesCount = 100;
        PlayerStatus.GoldCount = 100;
        PlayerStatus.ShipPos = new Vector3(0, 0, 0);
        PlayerStatus.ShipHealthMax = 100;
        PlayerStatus.ShipHealthCurrent = 100;
        PlayerStatus.AmmoCount = 20;

        QuestsStatus.testQuestStatus = -1;

        IslandStats.IslandLocations = GetComponent<IslandGeneration>().generateIslands();

        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }
}
