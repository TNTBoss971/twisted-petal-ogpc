using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
     // This variable shows which inventory cell the button corresponds to
    public int buttonID;
     // This variable is the descriptionID of this button's item
    public string description;
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
            gameObject.SetActive(false);
        }
        else
        {
            itemStored = InventoryManager.inventoryWeaponTypes[buttonID];
            gameObject.SetActive(true);
        }
        // Sets selected to true if the button has been pressed
        if (InventoryManager.selectedIDs.Contains(buttonID))
        {
            selected = true;
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            selected = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
        if (itemStored != null)
        {
            buttonImage.sprite = itemStored.GetComponent<GunController>().displayImage;
            description = itemStored.GetComponent<GunController>().description;
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
