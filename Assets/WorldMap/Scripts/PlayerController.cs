using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    public float speedMult;
    public int depletionRate;
    public float encounterChanceIncrease;

    public Text islandID;
    public Text resourcesCount;
    public Text goldCount;
    public Text deathAlert;
    public Text enterPrompt;

    public GameObject statsPanel;
    public GameObject savePanel;
    public GameObject GameOverPanel;
    public Text gameOverText;
    public GameObject encounterAlert;

    public int visitationSceneIndex;
    public int battleSceneIndex;

    public GameObject sprite;

    private Rigidbody2D player;

    private Vector2 speed;
    private Vector3 lastRotation;
    private int depletionCounter; //lower = faster
    private float cumulatingChance;

    private bool moveLock = false;
    private bool isVisiting;

    private float zRotation;

    private float encounterMult = 0; //Negative values make for more friendly areas; positive for more hostile

    public static void ReturnToMap(int goldReward, int resourcesReward, Vector3 returnPos) //if returnPos is the same location the ship was in before, pass in PlayerStatus.ShipPos
    {
        PlayerStatus.GoldCount += goldReward;
        PlayerStatus.ResourcesCount += resourcesReward;
        PlayerStatus.ShipPos = returnPos;

        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }

    public void SetVisiting(bool status) //Called whenever the player nears an island, to check if player can enter Island Visitation
    {
        isVisiting = status;
    }

    public void SetMoveLock(bool status) //Called whenever any sort of menu is opened or the player is no longer capable of moving
    {
        moveLock = status;
    }

    //Called when the ship enters or exits a friendly area
    //Give value of true upon enter; false upon exit
    //public void NearFriendly(bool val) 
    //{
    //    if (val) encounterMultFriendly = 1; //Convert boolean to integer
    //    if (!val) encounterMultFriendly = 0;
    //    //Therefore, resulting variable is 1 while in a friendly zone, and 0 while not
    //}

    ////Essentially the same as NearFriendly
    //public void NearHostile(bool val) //Called when the ship entes or exits a more hostile area
    //{
    //    if (val) encounterMultHostile = 1; //Convert boolean to integer
    //    if (!val) encounterMultHostile = 0;
    //}

    public void ChangeHostility(float val) //Called when the ship enters or exits the border of more hostile or more friendly areas
    {
        encounterMult += val;

    }

    void Start ()
    {
        player = GetComponent<Rigidbody2D>();

        transform.position = PlayerStatus.ShipPos;
        depletionCounter = 0;
        moveLock = false;

        cumulatingChance = -.05f;

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

        if((Input.GetButton("Menu")) && !moveLock && (player.velocity.x == 0) && (player.velocity.y == 0)) 
            //If moveLock is true, the player is either dead or in a menu. In either case, tab shouldn't open the stats panel
        {
            moveLock = true;

            goldCount.text = "";
            resourcesCount.text = "";

            statsPanel.SetActive(true);
        }

        if(Input.GetButton("Cancel") && !moveLock && (player.velocity.x == 0) && (player.velocity.y == 0))

        {
            moveLock = true;

            goldCount.text = "";
            resourcesCount.text = "";

            savePanel.SetActive(true);
        }

        if (!moveLock) //With the if statement, the gold count and resources count will be reset upon leaving the stats menu
        {
            SetGold();
            SetResources();
        }
    }

    private void FixedUpdate ()
    {
        float horVel = Input.GetAxis("Horizontal");
        float verVel = Input.GetAxis("Vertical");

        //sprite.transform.eulerAngles = lastRotation;

        if (!moveLock)
        {
            speed = new Vector2(horVel, verVel);
            player.velocity = speed * speedMult;
            //player.AddForce(speed * speedMult);
        }
        else player.velocity = new Vector2(0, 0); player.rotation = 0f;


        if(((horVel != 0) || (verVel != 0)) && !moveLock) //As long as a key is being pressed—!moveLock is included so it doesn't break in menus
        {
            zRotation = ((float)Math.Atan(player.velocity.y / player.velocity.x)) * (float)(180 / Math.PI);

            if (player.velocity.x >= 0)
            {
                lastRotation = new Vector3(0, 0, zRotation + 180);
                sprite.transform.eulerAngles = lastRotation;
            }
            else
            {
                lastRotation = new Vector3(0, 0, zRotation);
                sprite.transform.eulerAngles = lastRotation;
            }

            lastRotation = sprite.transform.eulerAngles;
        }
        

        if ((speed.x != 0) || (speed.y != 0)) //As long as the ship is in motion
        {
            depletionCounter++; //Deplete resources
            if ((depletionCounter >= depletionRate) && !moveLock)
            {
                PlayerStatus.ResourcesCount--;
                depletionCounter = 0;
            }

            //Generate a random number, and then multiply it by the area's hostility
            float rand = UnityEngine.Random.value * (1 + encounterMult); 

            if (rand > 1 - cumulatingChance) //Check for random encounter
            {
                cumulatingChance = -.025f;
                //EnemyStatus.ShipHealthMax = 50;
                //EnemyStatus.ShipHealthCurrent = 50;
                //EnemyStatus.GoldCount = 50;
                //EnemyStatus.ResourcesCount = 20;
                //SceneManager.LoadScene(SceneIndexes.Combat());
                //Commented out code is executed over in the method called below

                moveLock = true;
                encounterAlert.GetComponent<AlertManager>().dangerMult = encounterMult;
                encounterAlert.SetActive(true);
            }
            else if (cumulatingChance <= (.025 * (1 + encounterMult))) //The extra stuff in this if statement is so that cumulating chance can't be increased that much inside safer areas
            {
                cumulatingChance += encounterChanceIncrease;
            }
        }

        PlayerStatus.ShipPos = transform.position;
    }

    private void SetResources()
    {
        resourcesCount.text = "Resources: " + PlayerStatus.ResourcesCount.ToString();

        if(PlayerStatus.ResourcesCount <= 0)
        {
            moveLock = true;
            player.velocity = new Vector2(0, 0);
            gameOverText.text = "Your crew has run out of resources, and has starved to death. May the Gods have mercy on their souls.";

            GameOverPanel.SetActive(true);
        }

        if(PlayerStatus.ShipHealthCurrent <= 0)
        {
            moveLock = true;
            player.velocity = new Vector2(0, 0);
            gameOverText.text = "Your ship was destroyed in combat, and your crew sinks to the bottom of the ocean. May the Gods have mercy on their souls.";

            GameOverPanel.SetActive(true);
        }
    }

    private void SetGold()
    {
        goldCount.text = "Gold: " + PlayerStatus.GoldCount.ToString();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("island"))
    //    {
    //        IslandAttributes island = (IslandAttributes)collision.gameObject.GetComponent(typeof(IslandAttributes));
    //        islandID.text = island.GetName();


    //        isVisiting = true;
    //        PlayerStatus.VisitingIsland = island;

    //        enterPrompt.text = "Press Enter to dock.";
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("island"))
    //    {
    //        islandID.text = "";
    //        enterPrompt.text = "";

    //        isVisiting = false;
    //        PlayerStatus.VisitingIsland = null;
    //    }
    //}

}
