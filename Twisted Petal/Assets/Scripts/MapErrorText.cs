using UnityEngine;

public class MapErrorText : MonoBehaviour
{
    public MapManager map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (map.showError == true)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Equip atleast 1 weapon from the inventory before entering a level.";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
    }
}
