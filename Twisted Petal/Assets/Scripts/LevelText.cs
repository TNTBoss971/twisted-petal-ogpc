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
        {3, "Level 3: Moving Forward"},
        {4, "Level 4: Approaching the Bend"},
        {5, "Level 5: River's Junction"},
        {6, "Level 6: Home Stretch"},
        {7, "Level 7: Government Camp"},
        {8, "Level 8: Chart a Course"},
        {9, "Level 9: South of the City"},
        {10, "Level 10: Race to the Coast"},
        {11, "Level 11: Shelter From The Storm"},
        {12, "Level 12: On the Road Again"},
        {13, "Level 13: Through the Forest"},
        {14, "Level 14: Cannon Beach"},
        {15, "Level 15: Road to Astoria"}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MapManager.mapPosition < 1 || MapManager.mapPosition > 15)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level ?: How did we get here?";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = mapDesc[MapManager.mapPosition];
        }
    }
}
