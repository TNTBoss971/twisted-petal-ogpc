using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int testVarOne;
    public int testVarTwo;
    public int levelsBeaten;
    public List<GameObject> selectedItems;
    public List<GameObject> ownedItems;
    public int itemsLooted;
    public int itemsLootedOverall;
    public int enemiesBeaten;
    public int enemiesBeatenOverall;
    public List<int> selectedButtonIDs;
    public List<String> levelSummaries;
    public bool soundsMuted;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load

    public GameData()
    {
        this.levelsBeaten = 0;
        this.selectedItems = new List<GameObject>();
        ownedItems = new List<GameObject>();
        itemsLooted = 0;
        itemsLootedOverall = 0;
        enemiesBeaten = 0;
        enemiesBeatenOverall = 0;
        selectedButtonIDs = new List<int>();
        levelSummaries = new List<String>();
        soundsMuted = false;
    }
}
