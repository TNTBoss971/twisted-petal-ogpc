using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
     // This variable shows which inventory cell the button corresponds to
    public int buttonID;
     // This variable is the descriptionID of this button's item
    public int descriptionID;
    public Button button;
    private bool selected;
     // This variable is the actual item the button corresponds to
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
         // If the cell doesn't correspond to an item, it destroys itself
        if (InventoryManager.inventoryWeaponTypes.Count <= buttonID)
        {
            Destroy(gameObject);
        }
        else
        {
            itemStored = InventoryManager.inventoryWeaponTypes[buttonID];
        }
        // Sets selected to true if the button has been pressed
        if (InventoryManager.selectedIDs.Contains(buttonID))
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
        if (itemStored != null)
        {
            buttonImage.sprite = itemStored.GetComponent<GunController>().displayImage;
            descriptionID = itemStored.GetComponent<GunController>().descriptionID;
        }
    }

    void TaskOnClick()
    {
        if (selected == true)
        {
            InventoryManager.selectedItems.Remove(itemStored);
            InventoryManager.selectedIDs.Remove(buttonID);
            InventoryManager.selectedDescriptions.Remove(descriptionID);
        }
        else
        {
            if (InventoryManager.selectedItems.Count < 4)
            {
                InventoryManager.selectedItems.Add(itemStored);
                InventoryManager.selectedIDs.Add(buttonID);
                InventoryManager.selectedDescriptions.Add(descriptionID);
            }
        }
    }
}
