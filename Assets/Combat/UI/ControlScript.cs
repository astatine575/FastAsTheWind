using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScript : MonoBehaviour {

    public Button ButtonEnd; // points to Combat/Canvas/ButtonEnd
    public Text ButtonEndText; // points to Combat/Canvas/ButtonEnd/Text
    public Button ButtonFire0; // points to Combat/Canvas/ButtonFire
    public Button ButtonFire1;
    public Button ButtonFire2;
    public Button ButtonFire3;
    public Button ButtonEnemy0;
    public Button ButtonEnemy1;
    public Button ButtonEnemy2;

    public GameObject combatManager; // points to Combat/Combat Manager
    private CombatState combatState;
//<<<<<<< HEAD
    private bool IsWeaponSelected;
    private FirstCannon SelectedWeapon;
    private FirstCannon[] weapons;
    private GameObject target;
    public List<string> OrderTargets;
//=======
    private PlayerShipState playerShipState;
    private EnemyShipState enemyShipState;
   

//>>>>>>> develop

    // Use this for initialization
    void Start () {
        combatState = (CombatState)combatManager.GetComponent("CombatState"); // to get a non-static reference to the ViewScript Object


        ButtonFire0 = GameObject.Find("ButtonFire0").GetComponent<Button>();
        ButtonFire1 = GameObject.Find("ButtonFire1").GetComponent<Button>();
        ButtonFire2 = GameObject.Find("ButtonFire2").GetComponent<Button>();
        ButtonFire3 = GameObject.Find("ButtonFire3").GetComponent<Button>();

        ButtonEnemy0 = GameObject.Find("ButtonEnemy0").GetComponent<Button>();
        ButtonEnemy1 = GameObject.Find("ButtonEnemy1").GetComponent<Button>();
        ButtonEnemy2 = GameObject.Find("ButtonEnemy2").GetComponent<Button>();

        ButtonEnd = GameObject.Find("ButtonEnd").GetComponent<Button>();

        weapons = combatManager.GetComponent<CombatState>().player[0].GetComponents<FirstCannon>();

        IsWeaponSelected = false;
    }

    // Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space")) {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            else Time.timeScale = 0;
        }
        // TODO: update valid controls (i.e. if out of ammo or on cooldown, disable fire button)

        if (weapons.Length > 0 & IsWeaponSelected == false) { ButtonFire0.interactable = weapons[0].CanFire(); } else ButtonFire0.interactable = false;
        if (weapons.Length > 1 & IsWeaponSelected == false) { ButtonFire1.interactable = weapons[1].CanFire(); } else ButtonFire1.interactable = false;
        if (weapons.Length > 2 & IsWeaponSelected == false) { ButtonFire2.interactable = weapons[2].CanFire(); } else ButtonFire2.interactable = false;
        if (weapons.Length > 3 & IsWeaponSelected == false) { ButtonFire3.interactable = weapons[3].CanFire(); } else ButtonFire3.interactable = false;

        if (combatState.enemy.Count > 0 & IsWeaponSelected == true) { ButtonEnemy0.interactable = true; } else ButtonEnemy0.interactable = false;
        if (combatState.enemy.Count > 0 & IsWeaponSelected == true) { ButtonEnemy1.interactable = true; } else ButtonEnemy1.interactable = false;
        if (combatState.enemy.Count > 0 & IsWeaponSelected == true) { ButtonEnemy2.interactable = true; } else ButtonEnemy2.interactable = false;

        ButtonEnd.interactable = true;


        if (combatState.combatOver)
            ButtonEndText.text = "Return to World Map";
    }

    public void buttonFirePressed0 () { // called when the ButtonFire is pressed in the UI
        weapons[0].SelectWeapon();
        SelectedWeapon = weapons[0];
        IsWeaponSelected = true;
    }

    public void buttonFirePressed1()
    { // called when the ButtonFire is pressed in the UI
        weapons[1].SelectWeapon();
        SelectedWeapon = weapons[1];
        IsWeaponSelected = true;
    }

    public void buttonFirePressed2()
    { // called when the ButtonFire is pressed in the UI
        weapons[2].SelectWeapon();
        SelectedWeapon = weapons[2];
        IsWeaponSelected = true;
    }

    public void buttonFirePressed3()
    { // called when the ButtonFire is pressed in the UI
        weapons[3].SelectWeapon();
        SelectedWeapon = weapons[3];
        IsWeaponSelected = true;
    }

    public void buttonEnemyPressed0()
    { // called when the ButtonFire is pressed in the UI
        if (SelectedWeapon.NumOfTargets == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("HULL");
            SelectedWeapon.Target(target,copy);
            IsWeaponSelected = false;
        }

        if (SelectedWeapon.NumOfTargets == 2 & OrderTargets.Count == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("HULL");
            SelectedWeapon.Target(target, copy);
            IsWeaponSelected = false;
            OrderTargets.Clear();
        }

        else {
            OrderTargets.Add("HULL");
        }



    }

    public void buttonEnemyPressed1()
    { // called when the ButtonFire is pressed in the UI
        if (SelectedWeapon.NumOfTargets == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("SAIL");
            SelectedWeapon.Target(target,copy);
            OrderTargets.Clear();
            IsWeaponSelected = false;
        }

        if (SelectedWeapon.NumOfTargets == 2 & OrderTargets.Count == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("SAIL");
            SelectedWeapon.Target(target, copy);
            IsWeaponSelected = false;
            OrderTargets.Clear();
        }
        else
        {
            OrderTargets.Add("SAIL");
        }
    }

    public void buttonEnemyPressed2()
    { // called when the ButtonFire is pressed in the UI
        if (SelectedWeapon.NumOfTargets == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("CREW");
            SelectedWeapon.Target(target,copy);
            OrderTargets.Clear();
            IsWeaponSelected = false;
        }

        if (SelectedWeapon.NumOfTargets == 2 & OrderTargets.Count == 1)
        {
            target = combatState.enemy[0];
            List<string> copy = new List<string>(OrderTargets);
            copy.Add("CREW");
            SelectedWeapon.Target(target, copy);
            IsWeaponSelected = false;
            OrderTargets.Clear();
        }
        else
        {
            OrderTargets.Add("CREW");
        }
    }

    public void buttonEndPressed()
    { // called when ButtonEnd is pressed in the UI
        if (combatState.playerWon)
            PlayerController.ReturnToMap(EnemyStatus.GoldCount, EnemyStatus.ResourcesCount, PlayerStatus.ShipPos);
        else
            PlayerController.ReturnToMap(-1 * PlayerStatus.GoldCount / 2, -1 * PlayerStatus.ResourcesCount / 2, PlayerStatus.ShipPos);
    }

   


}
