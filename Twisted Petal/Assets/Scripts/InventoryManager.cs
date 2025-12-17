using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Overlays;
using UnityEngine.Rendering; 

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> startingWeapons = new List<GameObject>(); //if there are no weapons when loaded
    public List<GameObject> selectedItems = new List<GameObject>(); // This list contains the ids of all the currently selected items
    public List<int> selectedIDs = new List<int>(); // This list contains the ids of all the currently selected item ids
    public static List<int> selectedDescriptions = new List<int>(); // This list contains the descriptions of all currently selected buttons
    public List<GameObject> ownedItems; // This list contains the items the player currently has. Can be modified.
    private DataManagement saveData;
    public static List<GameObject> inventoryWeaponTypes; // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public List<GameObject> weaponTypes;
    public List<GameObject> lootedItems; // I don't know why we need this but everything breaks if you take it out
    private int itemsLooted; // how many items a player has looted
    private int lootLoop; // a temp variable used to keep track of how many loops to use when adding items
    private GameObject itemLooted; // the item that will be added to the player's inventory
    private bool hasAllItems = false;
    public GameObject buttonPrefab;
    public Canvas canvas;

    public SlotDisplayLogic[] displays;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        itemsLooted = saveData.itemsLooted;
        lootLoop = itemsLooted;
        //adds looted items to the lootedItems list
        if (itemsLooted > 0)
        {
            for (int i = 0; i < lootLoop; i++)
            {
                itemsLooted -= 1;
                itemLooted = weaponTypes[Random.Range(0, weaponTypes.Count)];
                lootedItems.Add(itemLooted);
            }
        }
        saveData.itemsLooted = itemsLooted;
        if (saveData.ownedItems.Count <= 0)
        {
            ownedItems = startingWeapons;
            saveData.ownedItems = ownedItems;
        }
        else
        {
            ownedItems = saveData.ownedItems;
        }
        lootLoop = lootedItems.Count;
        // adds items from lootedItems into the player's inventory.
        // this seems unnecessary but it doesn't work if you add them directly
        for (int i = 0; i < lootLoop; i++)
        {
            ownedItems.Add(lootedItems[i]);
        }

        // create the buttons that display the weapons
        int row = 0;
        int col = 0;

        for (int i = 0; i < ownedItems.Count; i++)
        {
            // clone the prefab
            GameObject clone = Instantiate(buttonPrefab, transform.position, transform.rotation);
            // set the parent
            clone.transform.SetParent(canvas.transform, false);
            // set the id
            clone.GetComponent<InventoryButton>().buttonID = i;
            // set the item stored
            clone.GetComponent<InventoryButton>().itemStored = ownedItems[i];
            // set the position
            clone.transform.position = new Vector2(row * 175 + 100f, col * -175 + 750);

            // 
            row++;
            if (row > 10)
            {
                col++;
                row = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        saveData.selectedItems = selectedItems;

        saveData.ownedItems = ownedItems;

        for (int i = 0; i < 4; i++)
        {
            if (selectedItems.Count > i)
            {
                displays[i].displayImage = selectedItems[i].GetComponent<GunController>().displayImage;
                displays[i].displayName = selectedItems[i].GetComponent<GunController>().weaponName;
                displays[i].displayDiscription = selectedItems[i].GetComponent<GunController>().description;
            }
            else
            {
                displays[i].displayImage = null;
                displays[i].displayName = null;
                displays[i].displayDiscription = null;
            }
        }
    }
}
