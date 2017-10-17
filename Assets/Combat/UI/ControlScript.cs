using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScript : MonoBehaviour {

    public Button ButtonEnd; // points to Combat/Canvas/ButtonEnd
    public Text ButtonEndText; // points to Combat/Canvas/ButtonEnd/Text
    public Button ButtonFire; // points to Combat/Canvas/ButtonFire
    public GameObject combatManager; // points to Combat/Combat Manager
    private CombatState combatState;
    private PlayerShipState playerShipState;
    private EnemyShipState enemyShipState;
   


    // Use this for initialization
    void Start () {
        combatState = (CombatState)combatManager.GetComponent("CombatState"); // to get a non-static reference to the ViewScript Object
        playerShipState = (PlayerShipState)combatState.player[0].GetComponent("PlayerShipState");
        enemyShipState = (EnemyShipState)combatState.player[0].GetComponent("EnemyShipState");
    }

    // Update is called once per frame
	void Update () {
        // TODO: update valid controls (i.e. if out of ammo or on cooldown, disable fire button)
        ButtonFire.interactable = playerShipState.canFire();
        if (combatState.combatOver)
            ButtonEndText.text = "Return to World Map";
    }

    public void buttonFirePressed () { // called when the ButtonFire is pressed in the UI
        playerShipState.attackTarget();
    }

    public void buttonEndPressed()
    { // called when ButtonEnd is pressed in the UI
        if (combatState.playerWon)
            PlayerController.ReturnToMap(EnemyStatus.GoldCount, EnemyStatus.ResourcesCount, PlayerStatus.ShipPos);
        else
            PlayerController.ReturnToMap(-1 * PlayerStatus.GoldCount / 2, -1 * PlayerStatus.ResourcesCount / 2, PlayerStatus.ShipPos);
    }

   


}
