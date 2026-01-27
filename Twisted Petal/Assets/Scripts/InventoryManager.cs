using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Overlays;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> startingWeapons = new List<GameObject>(); //if there are no weapons when loaded
    public List<GameObject> selectedItems = new List<GameObject>(); // This list contains the ids of all the currently selected items
    public List<int> selectedIDs = new List<int>(); // This list contains the ids of all the currently selected button ids
    public static List<int> selectedDescriptions = new List<int>(); // This list contains the descriptions of all currently selected buttons
    public List<GameObject> ownedItems; // This list contains the items the player currently has. Can be modified.
    private DataManagement saveData;
    public List<GameObject> weaponTypes;
    public static List<GameObject> inventoryWeaponTypes; // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public List<GameObject> lootedItems; // I don't know why we need this but everything breaks if you take it out
    private int itemsLooted; // how many items a player has looted
    private int lootLoop; // a temp variable used to keep track of how many loops to use when adding items
    private GameObject itemLooted; // the item that will be added to the player's inventory
    private bool hasAllItems = false;
    public GameObject buttonPrefab;
    public Canvas canvas;
    public SlotDisplayLogic[] displays;
    public int rarityChance;
    private int loopsDone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryWeaponTypes = weaponTypes;
        // higher number = rarer
        Dictionary<GameObject, int> itemRarities = new Dictionary<GameObject, int>
        {
            {inventoryWeaponTypes[0], 1},
            {inventoryWeaponTypes[1], 2},
            {inventoryWeaponTypes[2], 4},
            {inventoryWeaponTypes[3], 4},
            {inventoryWeaponTypes[4], 10},
            {inventoryWeaponTypes[5], 6},
            {inventoryWeaponTypes[6], 8}
        };
        saveData = this.GetComponent<DataManagement>();
        selectedItems = saveData.selectedItems;
        selectedIDs = saveData.selectedButtonIDs;
        itemsLooted = saveData.itemsLooted;
        lootLoop = itemsLooted;
        //adds looted items to the lootedItems list
        if (itemsLooted > 0)
        {
            while (loopsDone < lootLoop)
            {
                // picks a random number and random item. If the chosen
                // item's rarity is less than or equal to the random
                // number chosen then it gets added, if not, another loop.
                rarityChance = Random.Range(0, 14);
                itemLooted = weaponTypes[Random.Range(0, weaponTypes.Count)];
                if (itemRarities[itemLooted] <= rarityChance)
                {
                    lootedItems.Add(itemLooted);
                    itemsLooted -= 1;
                    loopsDone += 1;
                }
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

        saveData.selectedButtonIDs = selectedIDs;

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
