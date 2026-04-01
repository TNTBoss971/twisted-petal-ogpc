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
    private InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        buttonImage = this.GetComponent<Image>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the cell doesn't correspond to an item, it destroys itself
        if (inventoryManager.ownedItems.Count <= buttonID)
        {
            gameObject.SetActive(false);
        }
        else
        {
            itemStored = inventoryManager.ownedItems[buttonID];
            gameObject.SetActive(true);
        }
        // Sets selected to true if the button has been pressed
        if (inventoryManager.selectedIDs.Contains(buttonID))
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
            inventoryManager.selectedItems.Remove(itemStored);
            inventoryManager.selectedIDs.Remove(buttonID);
        }
        else
        {
            if (inventoryManager.selectedItems.Count < 4)
            {
                inventoryManager.selectedItems.Add(itemStored);
                inventoryManager.selectedIDs.Add(buttonID);
            }
        }
    }
}
