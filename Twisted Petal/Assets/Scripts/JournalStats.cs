using System.Collections.Generic;
using UnityEngine;

public class JournalStats : MonoBehaviour
{
    public int statID; // what stat text it corresponds to
    public Dictionary<int, string> statDisplayed = new Dictionary<int, string>();
    private DataManagement saveData;
    public static bool currentStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStats = true;
        saveData = this.GetComponent<DataManagement>();
        statDisplayed.Add(1, "Day: " + saveData.levelsBeaten);
        statDisplayed.Add(2, "Found " + saveData.itemsLooted + " item(s)");
        statDisplayed.Add(3, "Defeated " + saveData.enemiesBeaten + " enemies");
        statDisplayed.Add(4, "Found " + saveData.itemsLooted + " item(s) so far");
        statDisplayed.Add(5, "Defeated " + saveData.itemsLooted + " enemies so far");
    }

    // Update is called once per frame
    void Update()
    {
        if (statID == 1 || statID == 2 || statID == 3)
        {
            if (currentStats == true)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = statDisplayed[statID];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
        }
        else
        {
            if (currentStats == false)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = statDisplayed[statID];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
        }
    }
}
