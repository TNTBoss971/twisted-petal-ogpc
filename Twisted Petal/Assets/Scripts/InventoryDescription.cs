using UnityEngine;
using System.Collections.Generic;

public class InventoryDescription : MonoBehaviour
{
    // contains descriptions for each item ID.
    Dictionary<int, string> itemDesc = new Dictionary<int, string>
    {
        {1, "Red Item - It's Red!"},
        {2, "Greeb Item - It's Green!"},
        {3, "Blue Item - It's Blue!"},
        {4, "Unknown Item - It's Unknown!"},
        {5, "Unknown Item - It's Unknown!"},
        {6, "Unknown Item - It's Unknown!"},
        {7, "Unknown Item - It's Unknown!"},
        {8, "Unknown Item - It's Unknown!"},
        {9, "Unknown Item - It's Unknown!"},
        {10, "Unknown Item - It's Unknown!"},
        {11, "Unknown Item - It's Unknown!"},
        {12, "Unknown Item - It's Unknown!"},
        {13, "Unknown Item - It's Unknown!"},
        {14, "Unknown Item - It's Unknown!"},
        {15, "Unknown Item - It's Unknown!"},
        {16, "Unknown Item - It's Unknown!"}
    };
    public int descriptionID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the text to the description of the corresponding selected item
        if (descriptionID == 1)
        {
            if (InventoryManager.selectedItems.Count >= 1)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = itemDesc[InventoryManager.selectedItems[0]];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        if (descriptionID == 2)
        {
            if (InventoryManager.selectedItems.Count >= 2)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = itemDesc[InventoryManager.selectedItems[1]];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        if (descriptionID == 3)
        {
            if (InventoryManager.selectedItems.Count >= 3)
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = itemDesc[InventoryManager.selectedItems[2]];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        
    }
}
