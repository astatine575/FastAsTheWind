using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    public float speedMult;
    public int depletionRate;
    public float encounterChance;
    public int waitTime;
    public float fadeSpeed;

    public Text islandID;
    public Text resourcesCount;
    public Text goldCount;
    public Text deathAlert;
    public Text enterPrompt;
    public Text eventText;

    public GameObject statsPanel;
    public GameObject savePanel;

    public int visitationSceneIndex;
    public int battleSceneIndex;

    public GameObject sprite;

    private Rigidbody2D player;

    private Vector2 speed;
    private Vector3 lastRotation;
    private int depletionCounter; //lower = faster
    private float chanceHolder;

    private bool moveLock;
    private bool isVisiting;

    private float zRotation;

    public static void ReturnToMap(int goldReward, int resourcesReward, Vector3 returnPos) //if returnPos is the same location the ship was in before, pass in PlayerStatus.ShipPos
    {
        PlayerStatus.GoldCount += goldReward;
        PlayerStatus.ResourcesCount += resourcesReward;
        PlayerStatus.ShipPos = returnPos;

        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }

    public void SetVisiting(bool status)
    {
        isVisiting = status;
    }

    public void SetMoveLock(bool status)
    {
        moveLock = status;
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

        if (PlayerStatus.Beginning) StartCoroutine(FadeTutorial());
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
            if ((depletionCounter == depletionRate) && !moveLock)
            {
                PlayerStatus.ResourcesCount--;
                depletionCounter = 0;
            }

            
            float rand = UnityEngine.Random.value;

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
    }

    private IEnumerator FadeTutorial()
    {
        PlayerStatus.Beginning = false;
        //Fade in first instruction
        eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 0);
        eventText.text = "Use WASD keys to move";

        while (eventText.color.a < 1)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a + (Time.deltaTime * fadeSpeed));

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        //Fade out first instruction
        while (eventText.color.a > 0)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a - (Time.deltaTime * fadeSpeed));

            yield return null;
        }
        //Fade in second instruction
        eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 0);
        eventText.text = "Use tab to view stats and inventory";

        while (eventText.color.a < 1)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a + (Time.deltaTime * fadeSpeed));

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        //Fade out second instruction
        while (eventText.color.a > 0)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a - (Time.deltaTime * fadeSpeed));

            yield return null;
        }
        //Fade in third instruction
        eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 0);
        eventText.text = "Use esc to open the game menu";

        while (eventText.color.a < 1)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a + (Time.deltaTime * fadeSpeed));

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        //Fade out third instruction
        while (eventText.color.a > 0)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, eventText.color.a - (Time.deltaTime * fadeSpeed));

            yield return null;
        }
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
