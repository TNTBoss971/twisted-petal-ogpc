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
        {3, "Level 3: River's Junction"},
        {4, "Level 4: Home Stretch"},
        {5, "Level 5: Government Camp"},
        {6, "Level 6: Chart A Course"},
        {7, "Level 7: South of the City"},
        {8, "Level 8: Westward Bound"},
        {9, "Level 9: Race to the Coast"},
        {10, "Level 10: Shelter From The Storm"},
        {11, "Level 11: On The Road Again"},
        {12, "Level 12: Through the Forest"},
        {13, "Level 13: Cannon Beach"},
        {14, "Level 14: Seaside"},
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
