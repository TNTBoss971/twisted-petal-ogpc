using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Overlays;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> startingWeapons = new List<GameObject>(); //if there are no weapons when loaded
    public static List<GameObject> selectedItems = new List<GameObject>();
    // This list contains the ids of all the currently selected items
    public static List<int> selectedIDs = new List<int>();
    // This list contains the descriptions of all currently selected buttons
    public static List<int> selectedDescriptions = new List<int>();
    // This list contains the items the player currently has. Can be modified.
    public List<GameObject> ownedItems;
    private DataManagement saveData;
    // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public static List<GameObject> inventoryWeaponTypes;
    public List<GameObject> weaponTypes;
    public List<GameObject> lootedItems;
    private int itemsLooted;
    private int lootLoop; // a temp variable used to keep track of how many loops to use when adding items


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        itemsLooted = saveData.itemsLooted;
        lootLoop = itemsLooted;
        if (itemsLooted > 0)
        {
            for (int i = 0; i < lootLoop; i++)
            {
                itemsLooted -= 1;
                lootedItems.Add(weaponTypes[Random.Range(0, weaponTypes.Count)]);
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
            saveData.ownedItems = ownedItems;
        }
        lootLoop = lootedItems.Count;
        Debug.Log(lootedItems.Count);
        for (int i = 0; i < lootLoop; i++)
        {
            ownedItems.Add(lootedItems[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        saveData.selectedItems = selectedItems;
        inventoryWeaponTypes = ownedItems;
        saveData.ownedItems = ownedItems;
    }
}
