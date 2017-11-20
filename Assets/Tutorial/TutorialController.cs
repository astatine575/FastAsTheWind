using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    public float speedMult;
    public float requiredMovement;

    public Text islandID;
    public Text resourcesCount;
    public Text goldCount;
    public Text enterPrompt;
    public Text tutorText;

    public GameObject statsPanel;
    public GameObject savePanel;
    public GameObject tutorPanel;

    public GameObject sprite;

    private Rigidbody2D player;

    private Vector2 speed;
    private Vector3 lastRotation;
    private int requiredMovementCounter = 0;
    private float cumulatingChance;

    private bool moveLock = false;
    private bool isVisiting = false;
    private string tutorStatus = "start";
    //Possible states: "start", "move", "visit", "stats", "menu", "conclusion"

    private float zRotation;

    private float encounterMult = 0; //Negative values make for more friendly areas; positive for more hostile


    public void SetMoveLock(bool status) //Called whenever any sort of menu is opened or the player is no longer capable of moving
    {
        moveLock = status;
    }

    public void AdvanceTutorial()
    {
        switch(tutorStatus)
        {
            case "start":
                tutorStatus = "move";
                StartMove();
                break;
            case "move":
                tutorStatus = "visit";
                StartVisit();
                break;
            case "visit":
                tutorStatus = "stats";
                StartStats();
                break;
            case "stats":
                tutorStatus = "menu";
                StartMenu();
                break;
            case "menu":
                tutorStatus = "conclusion";
                StartConclusion();
                break;
            case "conclusion":
                ConcludeTutorial();
                break;
        }
    }

    public string GetTutorStatus()
    {
        return tutorStatus;
    }

    public void SetVisiting(bool status)
    {
        isVisiting = status;
    }

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        transform.position = PlayerStatus.ShipPos;
        moveLock = false;

        cumulatingChance = -.05f;

        islandID.text = "";
        enterPrompt.text = "";

        goldCount.text = "Gold: 3862";
        resourcesCount.text = "Resources: 784";

        PlayerStatus.PlayerControllerRef = (PlayerController)gameObject.GetComponent(typeof(PlayerController));

        AdvanceTutorial();
    }

    private void Update()
    {
        if ((Input.GetButton("Submit")) && isVisiting && tutorStatus == "visit")
        {
            AdvanceTutorial();
        }

        if ((Input.GetButton("Menu")) && !moveLock && (player.velocity.x == 0) && (player.velocity.y == 0) && tutorStatus == "stats")
        //If moveLock is true, the player is either dead or in a menu. In either case, tab shouldn't open the stats panel
        {
            moveLock = true;

            goldCount.text = "Gold: 3862";
            resourcesCount.text = "Resources: 784";

            statsPanel.SetActive(true);
        }

        if (Input.GetButton("Cancel") && !moveLock && (player.velocity.x == 0) && (player.velocity.y == 0) && tutorStatus == "menu")

        {
            moveLock = true;

            goldCount.text = "Gold: 3862";
            resourcesCount.text = "Resources: 784";

            savePanel.SetActive(true);
        }

        if (!moveLock) //With the if statement, the gold count and resources count will be reset upon leaving the stats menu
        {
            goldCount.text = "Gold: 3862";
            resourcesCount.text = "Resources: 784";
        }
    }

    private void FixedUpdate()
    {
        float horVel = Input.GetAxis("Horizontal");
        float verVel = Input.GetAxis("Vertical");

        //sprite.transform.eulerAngles = lastRotation;

        if (!moveLock && (tutorStatus == "move" || tutorStatus == "visit"))
        {
            
            speed = new Vector2(horVel, verVel);
            player.velocity = speed * speedMult;
            //player.AddForce(speed * speedMult);
        }
        else player.velocity = new Vector2(0, 0);

        if (((horVel != 0) || (verVel != 0)) && !moveLock && (tutorStatus == "move" || tutorStatus == "visit")) //As long as a key is being pressed—!moveLock is included so it doesn't break in menus
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

            if (tutorStatus == "move")
            {
                tutorText.text = "\"See? You got the hang of this. Now, just sail around a bit more, " +
                    "so you get the feel of it.\"";
                requiredMovementCounter++;
                if (requiredMovementCounter >= requiredMovement) AdvanceTutorial();
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (tutorStatus == "visit") tutorText.text = "\"When you get close enough to an island, you can dock at that island by pressing enter." +
    //            " Why don't you try that now?\"";

    //}

    private void StartMove()
    {
        tutorText.text = "\"Why don't you try sailing the ship yourself? I think you're old enough by now, don't you?" +
            " Just use the W, A, S, and D keys.\"";
    }

    private void StartVisit()
    {
        tutorText.text = "\"Well done! Now that you've got the basics down, why don't you brings us over to the coast?\"";
    }

    private void StartStats()
    {
        tutorText.text = "\"Yes, just like that! No need to go into port yet, though. There's still something I want to show you." +
            " Press that tab button for me, if you will.\"";
    }

    private void StartMenu()
    {
        tutorText.text = "\"Yes, yes, I know; this isn't terribly interesting. But there are some things a competent sailor simply" +
            " must know. Now, press the escape button for me—go on, now.\"";
    }

    private void StartConclusion()
    {
        tutorText.text = "\"Before we head back to your mother, there's one last thing I need to tell you.\"" +
            " (press ENTER to continue)";
        StartCoroutine(GoldAndResources());
    }

    private void ConcludeTutorial()
    {
        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }

    private IEnumerator GoldAndResources()
    {
        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"Two of the most important things you'll need to keep an eye on as the captain of a ship are gold, " +
            "and resources.\" (press ENTER to continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"Gold is the universal currency of the world, and can get you pretty much anything you want. Keep in " +
            "mind: all the gold in the world won't do you a lick of good out in the middle of the ocean.\" (press ENTER to continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"Your resources—that is, your food, water, and medicine—are what you'll need to keep your men alive " +
            "during your journeys far from land. You'll be able to live without gold, but without resources you may as well" +
            " toss yourself in the sea and let the sharks have you.\" (press ENTER to continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"You can see how much gold and resources you have up in the top corners of the screen." +
            " Keep an eye on those numbers, so you don't get caught off guard!\" (press ENTER to continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "\"That's about all I got to say to you, I suppose. That wasn't so bad, now was it? Well, why don't we " +
            "head home now? I can teach you more tomorrow, if you like.\" (press ENTER to continue)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        tutorText.text = "(press ENTER to skip your father's long series of financially crippling misfortunes, which threw your " +
            "family into poverty and funnelled you into a life of crime)";

        yield return new WaitForSeconds(.75f);
        yield return new WaitUntil(() => Input.GetButton("Submit") == true);

        AdvanceTutorial();
    }
}
