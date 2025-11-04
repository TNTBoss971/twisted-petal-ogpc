using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the item description to that of the currently selected item.

        GetComponent<TMPro.TextMeshProUGUI>().text = InventoryButton.currentDesc;
    }
}
