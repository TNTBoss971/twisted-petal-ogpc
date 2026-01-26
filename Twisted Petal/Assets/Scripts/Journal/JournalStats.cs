using System.Collections.Generic;
using UnityEngine;

public class JournalStats : MonoBehaviour
{
    public int statID; // what stat text it corresponds to
    public static bool currentStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStats = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (statID == 1 || statID == 2 || statID == 3)
        {
            if (currentStats == true)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = JournalManager.statDisplayed[statID];
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
                GetComponent<TMPro.TextMeshProUGUI>().text = JournalManager.statDisplayed[statID];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
        }
    }
}
