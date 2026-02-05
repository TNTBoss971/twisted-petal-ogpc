using UnityEngine;
using System.Collections.Generic;

public class InventoryDescription : MonoBehaviour
{
    // contains descriptions for each item ID.
    Dictionary<int, string> itemDesc = new Dictionary<int, string>
    {
        {0, "White Item - It's White!"},
        {1, "Red Item - It's Red!"},
        {2, "Blue Item - It's Blue!"},
        {3, "Greeb Item - It's Green!"},
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
        {15, "Unknown Item - It's Unknown!"}
    };
    
    public int descriptionID;
    public string description;
    private InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();     
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the text to the description of the corresponding selected item
        if (descriptionID == 1)
        {
            if (inventoryManager.selectedItems.Count >= 1)
            {
                //GetComponent<TMPro.TextMeshProUGUI>().text = InventoryManager.selectedDescriptions[0];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        if (descriptionID == 2)
        {
            if (inventoryManager.selectedItems.Count >= 2)
            {
                //GetComponent<TMPro.TextMeshProUGUI>().text = InventoryManager.selectedDescriptions[1];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        if (descriptionID == 3)
        {
            if (inventoryManager.selectedItems.Count >= 3)
            {
                //GetComponent<TMPro.TextMeshProUGUI>().text = InventoryManager.selectedDescriptions[2];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        if (descriptionID == 4)
        {
            if (inventoryManager.selectedItems.Count >= 4)
            {
                //GetComponent<TMPro.TextMeshProUGUI>().text = InventoryManager.selectedDescriptions[3];
            }
            else
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = "None Selected";
            }
        }
        
    }
}
