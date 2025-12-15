using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    // contains every level description
    Dictionary<int, string> mapDesc = new Dictionary<int, string>
    {
        {1, "Level 1: Flee the City"},
        {2, "Level 2: On the Open Road"},
        {3, "Level 3: Driving Some More"},
        {4, "Level 4: Wow It's Mount Hood"},
        {5, "Level 5: The 3/4 Mark "},
        {6, "Level 6: Im running out of names"},
        {7, "Level 7: Wow we're almost at the island"}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MapManager.mapPosition < 1 || MapManager.mapPosition > 7)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level ?: How did we get here?";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = mapDesc[MapManager.mapPosition];
        }
    }
}
