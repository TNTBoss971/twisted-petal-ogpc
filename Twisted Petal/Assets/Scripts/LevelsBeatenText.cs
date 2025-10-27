using UnityEngine;

public class LevelsBeatenText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Levels Beaten: " + MapManager.levelsBeaten;
    }
}
