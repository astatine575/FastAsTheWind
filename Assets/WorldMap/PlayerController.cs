using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speedMult;
    public int depletionRate;
    public float encounterChance;

    public Text islandID;
    public Text resourcesCount;
    public Text goldCount;
    public Text deathAlert;
    public Text enterPrompt;

    public int visitationSceneIndex;
    public int battleSceneIndex;


    private Rigidbody2D player;

    private int depletionCounter; //lower = faster
    private float chanceHolder;

    private bool moveLock;
    private bool isVisiting;

    public static void ReturnToMap(int goldReward, int resourcesReward, Vector3 returnPos) //if returnPos is the same location the ship was in before, pass in PlayerStatus.ShipPos
    {
        PlayerStatus.GoldCount += goldReward;
        PlayerStatus.ResourcesCount += resourcesReward;
        PlayerStatus.ShipPos = returnPos;

        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }

    void Start ()
    {
        player = GetComponent<Rigidbody2D>();

        transform.position = PlayerStatus.ShipPos;
        depletionCounter = 0;
        moveLock = false;

        chanceHolder = encounterChance;

        islandID.text = "";
        deathAlert.text = "";
        enterPrompt.text = "";

        SetResources();
        SetGold();

        PlayerStatus.PlayerControllerRef = (PlayerController)gameObject.GetComponent(typeof(PlayerController));
    }

    private void Update()
    {
        if((Input.GetButton("Submit")) && isVisiting)
        {
            SceneManager.LoadScene(SceneIndexes.IslandVisitation());
        }
    }

    private void FixedUpdate ()
    {
        float horVel = Input.GetAxis("Horizontal");
        float verVel = Input.GetAxis("Vertical");


        if (!moveLock)
        {
            Vector2 speed = new Vector2(horVel, verVel);
            player.velocity = speed * speedMult;
        }

        if((horVel != 0) || (verVel != 0))
        {
            depletionCounter++; //Deplete resources
            if ((depletionCounter == depletionRate) && !moveLock)
            {
                PlayerStatus.ResourcesCount--;
                depletionCounter = 0;
            }

            
            float rand = Random.value;

            if (rand > 1 - chanceHolder) //Check for random encounter
            {
                chanceHolder = .0001f;
                EnemyStatus.ShipHealthMax = 50;
                EnemyStatus.ShipHealthCurrent = 50;
                EnemyStatus.GoldCount = 50;
                EnemyStatus.ResourcesCount = 20;
                SceneManager.LoadScene(SceneIndexes.Combat());
            }
            else chanceHolder += encounterChance;
        }

        PlayerStatus.ShipPos = transform.position;

        SetGold();
        SetResources();
    }

    private void SetResources()
    {
        resourcesCount.text = "Resources: " + PlayerStatus.ResourcesCount.ToString();

        if(PlayerStatus.ResourcesCount <= 0)
        {
            moveLock = true;
            player.velocity = new Vector2(0, 0);
            deathAlert.text = "Your crew has run out of resources, and has starved to death. May the Gods have mercy on their souls.";
        }
    }

    private void SetGold()
    {
        goldCount.text = "Gold: " + PlayerStatus.GoldCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("island"))
        {
            IslandAttributes island = (IslandAttributes)collision.gameObject.GetComponent(typeof(IslandAttributes));
            islandID.text = island.GetName();

            island.SetDiscovered();

            isVisiting = true;
            PlayerStatus.VisitingIsland = island;

            enterPrompt.text = "Press Enter to dock.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("island"))
        {
            islandID.text = "";
            enterPrompt.text = "";

            isVisiting = false;
            PlayerStatus.VisitingIsland = null;
        }
    }

}
