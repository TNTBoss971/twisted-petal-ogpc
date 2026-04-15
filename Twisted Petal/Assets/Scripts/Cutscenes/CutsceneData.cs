using UnityEngine;
using System.Collections.Generic;

public class CutsceneData : MonoBehaviour
{
    public List<string> dialogueLines; // every dialogue line
    public List<Sprite> dialoguePortraits; // every dialogue portrait
    public List<decisionType> decisions; // every cutscene decision
    public enum decisionType
    {
        None, //default, no decision
        SupplyCache, //just gives 3 supplies
        SupplyConflict, //should you take some or all?
        ThinkBack, // thinking back to the supply conflict
        Charity, // some travelers ask for supplies
        Market, // pesticide sprayer for sale
        Ponder, // dialogue changes based on previous actions
        BarrenCache, // leave or take supplies
        LaserTurret, // take the turret as a weapon or leave it?
        WallRepair, // sacrifice max hp to help people?
        TheEnding // which ending did you get?
    }

    public List<string> altLinesOne; // alternate lines of dialogue
    public List<string> altLinesTwo; // alternate lines of dialogue

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
