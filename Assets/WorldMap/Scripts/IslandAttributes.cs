using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IslandAttributes : MonoBehaviour
{

    public string islandName;
    public int islandID;
    public bool hasBlacksmith;
    public bool hasGunsmith;
    public bool hasTavern;
    public bool hasWardsmith;
    public bool hasArchetier;

    public bool hasSpecial; //No need to set this to true, ever
    public string specialText; //Not necessary if not an island with a special button
    public string specInfo;
    public string specInfoResponse;
    private int actions;

    private GameObject activePanel;

    public bool specialVisible;
    // private static bool isDiscovered;

    [HideInInspector]
    public Inventory shopInventory;
    public Inventory gunsmithInventory;

    // Use this for initialization
    public virtual void Start()
    {
        specialVisible = false;
        SetActions(0);
        //isDiscovered = false;
        specialVisible = false;
        transform.position = IslandStats.IslandLocations[islandID];

        //Adding items to inventory
        ShopSetup();
    }


    public void SetAttributes(string name, bool blackS, bool gunS, bool tav, bool wardS, bool arch, bool special)
    {
        islandName = name; hasBlacksmith = blackS; hasGunsmith = gunS; hasTavern = tav;
        hasWardsmith = wardS; hasArchetier = arch; hasSpecial = special;
    }

    //public void SetDiscovered()
    //{
    //    isDiscovered = true;
    //}

    //public bool GetDiscovered()
    //{
    //    return isDiscovered;
    //}

    public string GetName()
    {
        return islandName;
    }

    public void SetActivePanel(GameObject panel)
    {
        activePanel = panel;
    }

    public GameObject GetActivePanel()
    {
        return activePanel;
    }

    public void SetActions(int actionsNum)
    {
        actions = actionsNum;
    }

    public int GetActions()
    {
        return actions;
    }

    public Inventory GetShop() //This returns an inventory, because in the future we may want to have shops be depletable
    {
        return shopInventory;
    }

    //Below are all the virtual classes


    public virtual bool CheckSpecial() //Here, in subclass, have this return true if the requirements have been met for seeing the special button
    {
        return false;
    }

    public virtual string GetSpecInfoButtonText()
    {
        return "NONE";
    }

    public virtual string GetSpecInfoResponse()
    {
        return "";
    }

    public virtual string GetRumors() //In subclass, return a rumor string of your choice, as well as initiate any changes caused by that/those rumor/s
    {
        return null;
    }

    public virtual void ShopSetup()
    {
        //Market shop
        shopInventory = new Inventory();

        shopInventory.AddItem(new CrewWeapon(
                                            "Sword",                        // name
                                            "A four-foot long steel blade with a leather-bound hilt. A well made sword, but nothing to brag about.", // description
                                            50,                             // value
                                            CrewWeapon.WeaponMaterial.STEEL,// weapon material
                                            100,                            // current condition
                                            100,                            // maximum condition
                                            2));                            // damage

        shopInventory.AddItem(new Consumable(
                                            "Health Potion",                // name
                                            "A blood-red potion, used to magically heal an individual's wounds.", // description
                                            5,                              // value
                                            Consumable.Effect.HEAL,         // effect
                                            10),                            // magnitude
                                            4);                             // quantity
        //Gunsmith shop
        gunsmithInventory = new Inventory();

        gunsmithInventory.AddItem(new Ammunition(
                                            "Explosive Round",             // name
                                            "Filled to the brim with brimstone, these cannonballs violently explode on impact, greviously wounding enemy crew.", // description
                                            2,                             // value
                                            Ammunition.AmmoType.CANNONBALL, // weapon material
                                            1,                              // hull damage multiplier
                                            3,                              // crew damage multiplier
                                            1),                             // sail damage multiplier
                                            500);                           // quantity
        gunsmithInventory.AddItem(new ShipWeapon(
                                        "Basic Cannon",   // name
                                        "The old and reliable 20mm naval gun, a common weapon used by merchant, pirate, and naval vessels alike.", // description
                                        125,                             // value
                                        ShipWeapon.WeaponType.CANNON,   // weapon type
                                        Ammunition.AmmoType.CANNONBALL, // ammo type
                                        2,                              // cooldown
                                        10));                           // damage
    }

    //What the buttons in the visitation scene do, which can be overwritten by a subclass of IslandAttributes
    public virtual void MarketOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.relevantStatText.text = "Resources: " + PlayerStatus.ResourcesCount.ToString();
        caller.interactionsText.text = "Whatcha buyin', stranger?";

        activePanel = caller.linkedPanel;
    }

    public virtual void BlacksmithOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.interactionsText.text = "Need yourself some steel? You've come to the right place.";

        activePanel = caller.linkedPanel;
    }

    public virtual void GunsmithOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.relevantStatText.text = "";//"Cannonballs: " + PlayerStatus.AmmoCount.ToString();
        caller.interactionsText.text = "Looking to defend yourself, huh? I got just the thing.";

        activePanel = caller.linkedPanel;
    }

    public virtual void WardsmithOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.interactionsText.text = "Ahhhh. Welcome, good sir. You've come to the right store; the other wardsmiths on this island " +
            "are contemptible amateurs.";

        activePanel = caller.linkedPanel;
    }

    public virtual void ArchetierOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.interactionsText.text = "It may not be as flashy, but a bow in the right hands is worth a dozen muskets. " +
            "Anything I can get for you?";

        activePanel = caller.linkedPanel;
    }

    public virtual void TavernOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        caller.interactionsText.text = "*The tavern is filled with rowdy drunks, singing along with a bard who looks like he'd" +
            "rather be elsewhere. The bartender greets you warmly, offering food and drinks (for an outrageous price, of course).*";

        activePanel = caller.linkedPanel;
    }

    public virtual void ShipswrightOnClick(MainPanelButton caller)
    {
        caller.mainPanel.SetActive(false);
        caller.linkedPanel.SetActive(true);

        if (PlayerStatus.Ship._hullHealth == PlayerStatus.Ship.HullStrengthMax())
            caller.interactionsText.text = "Looking to upgrade your ship, eh?";
        else caller.interactionsText.text = "Yer ship's lookin' a little rough there. " +
                "Want me to patch 'er up fer ya?";

        caller.goldCountText.text = "Gold: " + PlayerStatus.GoldCount;
        caller.relevantStatText.text = "Ship Health: " + PlayerStatus.Ship._hullHealth + "/" + PlayerStatus.Ship.HullStrengthMax();

        activePanel = caller.linkedPanel;
    }

    public virtual void SpecialOnClick(MainPanelButton caller) //Generic islands have no special button
    {
        Debug.Log("This shouldn't be here");
    }

    public virtual void SetSailOnClick(MainPanelButton caller)
    {
        SceneManager.LoadScene(SceneIndexes.WorldMap());
    }


    //Potential actions that can result from dialogue in the visitation sequences
    public virtual void TriggerDialogueConsequences(bool[] actions)
    {
        //These are just here to quickly create them in child classes
        //if(actions[0] == true)
        //{
        //    ActionZero();
        //}

        //if (actions[1] == true)
        //{
        //    ActionOne();
        //}

        //if (actions[2] == true)
        //{
        //    ActionTwo();
        //}

        //if (actions[3] == true)
        //{
        //    ActionThree();
        //}

        //if (actions[4] == true)
        //{
        //    ActionFour();
        //}

        //if (actions[5] == true)
        //{
        //    ActionFive();
        //}

        //if (actions[6] == true)
        //{
        //    ActionSix();
        //}

        //if (actions[7] == true)
        //{
        //    ActionSeven();
        //}
    }

    //public void ActionZero()
    //{

    //}

    //public void ActionOne()
    //{

    //}

    //public void ActionTwo()
    //{

    //}

    //public void ActionThree()
    //{

    //}

    //public void ActionFour()
    //{

    //}

    //public void ActionFive()
    //{

    //}

    //public void ActionSix()
    //{

    //}

    //public void ActionSeven()
    //{

    //}

}

