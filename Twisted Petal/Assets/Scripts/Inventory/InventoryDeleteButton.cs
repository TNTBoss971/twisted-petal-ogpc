using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDeleteButton : MonoBehaviour
{
    private Button button;
    public InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void TaskOnClick()
    {
        // When pressed, scraps selected items
        if (inventoryManager.GetComponent<DataManagement>().ownedItems.Count > inventoryManager.selectedItems.Count)
        {
            if (inventoryManager.GetComponent<DataManagement>().ownedItems.Count > inventoryManager.startingWeapons.Count)
            {
                for (int i = 0; i < inventoryManager.selectedItems.Count; i++)
                {
                    inventoryManager.ownedItems.Remove(inventoryManager.selectedItems[i]);
                    inventoryManager.GetComponent<DataManagement>().supplies += 1;
                }
            }
        }
        inventoryManager.selectedItems.Clear();
        inventoryManager.selectedIDs.Clear();
    }
}
