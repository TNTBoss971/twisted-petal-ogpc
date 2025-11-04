using UnityEngine;

public class LevelText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the level text depending on the player's currently selected level on the map

        if (MapManager.mapPosition == 1)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 1: Flee the City";
        }
        else if (MapManager.mapPosition == 2)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 2: On the Open Road";
        }
        else if (MapManager.mapPosition == 3)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 3: Driving Some More";
        }
        else if (MapManager.mapPosition == 4)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 4: Wow It's Mount Hood";
        }
        else if (MapManager.mapPosition == 5)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 5: The 3/4 Mark ";
        }
        else if (MapManager.mapPosition == 6)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 6: Im running out of names";
        }
        else if (MapManager.mapPosition == 7)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level 7: Wow we're almost at the island";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level ?: how did u get here lol";
        }
        
    }
}
