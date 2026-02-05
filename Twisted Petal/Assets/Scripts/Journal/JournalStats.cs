using System.Collections.Generic;
using UnityEngine;

public class JournalStats : MonoBehaviour
{
    public int statID; // what stat text it corresponds to
    public bool showOverallStats;
    public static bool statsHidden = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (statsHidden == false)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = JournalManager.statDisplayed[statID];
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
    }
}
