using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public int buttonID;
    public Button button;
    private bool selected;
    public GameObject itemStored;
    private Image buttonImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        buttonImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.inventoryWeaponTypes.Count <= buttonID)
        {
            Destroy(gameObject);
        }
        else
        {
            itemStored = InventoryManager.inventoryWeaponTypes[buttonID];
        }
        // Changes item to green if it's selected.
        if (InventoryManager.selectedItems.Contains(itemStored))
        {
            selected = true;
            gameObject.GetComponent<Image>().color = Color.green;
            if (InventoryManager.selectedIDs.Contains(buttonID) == false)
            {
                InventoryManager.selectedIDs.Add(buttonID);
            }
        }
        else
        {
            selected = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    void TaskOnClick()
    {
        if (selected == true)
        {
            InventoryManager.selectedItems.Remove(itemStored);
            InventoryManager.selectedIDs.Remove(buttonID);
        }
        else
        {
            if (InventoryManager.selectedItems.Count < 4)
            {
                InventoryManager.selectedItems.Add(itemStored);
                InventoryManager.selectedIDs.Add(buttonID);
            }
        }
    }
}
