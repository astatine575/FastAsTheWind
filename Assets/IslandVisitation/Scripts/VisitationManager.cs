using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitationManager : MonoBehaviour {

    public Button bSmith;
    public Button gSmith;
    public Button tavern;
    public Button wSmith;
    public Button archetier;
    public Button spec;

    public Text relevantStatCount;
    public Text goldCount;

    // Use this for initialization
    void Start ()
    {
        relevantStatCount.text = "";
        goldCount.text = "";

        if (!PlayerStatus.VisitingIsland.hasBlacksmith) bSmith.gameObject.SetActive(false);
        if (!PlayerStatus.VisitingIsland.hasGunsmith) gSmith.gameObject.SetActive(false);
        if (!PlayerStatus.VisitingIsland.hasTavern) tavern.gameObject.SetActive(false);
        if (!PlayerStatus.VisitingIsland.hasWardsmith) wSmith.gameObject.SetActive(false);
        if (!PlayerStatus.VisitingIsland.hasArchetier) archetier.gameObject.SetActive(false);
        if (!PlayerStatus.VisitingIsland.specialVisible) spec.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerStatus.VisitingIsland.CheckSpecial()) spec.gameObject.SetActive(true);
    }
}
