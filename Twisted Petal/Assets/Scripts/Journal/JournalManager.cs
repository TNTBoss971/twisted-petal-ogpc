using UnityEngine;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    public static Dictionary<int, string> statDisplayed = new Dictionary<int, string>();
    private DataManagement saveData;
    private bool itemsAdded = false;
    public List<GameObject> startingWeapons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statDisplayed.Clear();
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.ownedItems.Count <= 0)
        {
            saveData.ownedItems = startingWeapons;
        }
        if (itemsAdded == false)
        {
            statDisplayed.Add(1, "Day: " + saveData.levelsBeaten);
            statDisplayed.Add(2, "Found " + saveData.itemsLooted + " item(s)");
            statDisplayed.Add(3, "Defeated " + saveData.enemiesBeaten + " enemies");
            statDisplayed.Add(4, "Found " + saveData.itemsLootedOverall + " item(s) so far");
            statDisplayed.Add(5, "Defeated " + saveData.enemiesBeatenOverall + " enemies so far");
            itemsAdded = true;
        }
    }
}
