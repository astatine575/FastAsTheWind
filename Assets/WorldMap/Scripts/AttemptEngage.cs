using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttemptEngage : MonoBehaviour
{
    public Button thisButton;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        thisButton.onClick.AddListener(OnClickFlee);
    }

    private void OnClickFlee()
    {
        //TODO: incorporate the possibility of a variety of enemy ships
        //TODO: add some sort of randomness and a chance to fail, for if the ship is not hostile

        EnemyStatus.ShipHealthMax = 50;
        EnemyStatus.ShipHealthCurrent = 50;
        EnemyStatus.GoldCount = 50;
        EnemyStatus.ResourcesCount = 20;
        SceneManager.LoadScene(SceneIndexes.Combat());
    }
}
