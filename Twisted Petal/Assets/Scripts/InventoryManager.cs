using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static List<GameObject> selectedItems = new List<GameObject>();
     // This list contains the ids of all the currently selected buttons
    public static List<int> selectedIDs = new List<int>();
     // This list contains the descriptions of all currently selected buttons
    public static List<int> selectedDescriptions = new List<int>();
    // This list contains the items the player currently has. Can be modified.
    public List<GameObject> ownedItems;
    private DataManagement saveData;
    // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public static List<GameObject> inventoryWeaponTypes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        saveData.selectedItems = selectedItems;
        saveData.ownedItems = ownedItems;
        inventoryWeaponTypes = ownedItems;
    }
}
