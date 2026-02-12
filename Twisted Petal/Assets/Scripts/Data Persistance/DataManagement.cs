using UnityEngine;
using System.Collections.Generic;
using System;

public class DataManagement : MonoBehaviour, IDataPersistance
{
    public int levelsBeaten = 0;
    public List<GameObject> selectedItems;
    public List<GameObject> ownedItems;
    public int itemsLooted;
    public int itemsLootedOverall;
    public int enemiesBeaten;
    public int enemiesBeatenOverall;
    public List<int> selectedButtonIDs;
    public List<String> levelSummaries;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(GameData data)
    {
        levelsBeaten = data.levelsBeaten;
        selectedItems = data.selectedItems;
        ownedItems = data.ownedItems;
        itemsLooted = data.itemsLooted;
        itemsLootedOverall = data.itemsLootedOverall;
        enemiesBeaten = data.enemiesBeaten;
        enemiesBeatenOverall = data.enemiesBeatenOverall;
        selectedButtonIDs = data.selectedButtonIDs;
        levelSummaries = data.levelSummaries;
    }

    public void SaveData(ref GameData data)
    {
        data.levelsBeaten = levelsBeaten;
        data.selectedItems = selectedItems;
        data.ownedItems = ownedItems;
        data.itemsLooted = itemsLooted;
        data.itemsLootedOverall = itemsLootedOverall;
        data.enemiesBeaten = enemiesBeaten;
        data.enemiesBeatenOverall = enemiesBeatenOverall;
        data.selectedButtonIDs = selectedButtonIDs;
        data.levelSummaries = levelSummaries;
    }
}
