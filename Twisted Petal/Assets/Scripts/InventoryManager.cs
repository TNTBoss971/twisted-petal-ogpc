using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public static List<GameObject> selectedItems = new List<GameObject>();
     // This list contains the ids of all the currently selected buttons
    public static List<int> selectedIDs = new List<int>();
    // This list contains the items the player currently has. Can be modified.
    public List<GameObject> ownedItems;
    private DataManagement saveData;
    // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public static List<GameObject> inventoryWeaponTypes;

    public SlotDisplayLogic[] displays;
    
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

        for (int i = 0; i < 4; i++)
        {
            if (selectedItems.Count > i)
            {
                displays[i].displayImage = selectedItems[i].GetComponent<GunController>().displayImage;
                displays[i].displayName = selectedItems[i].GetComponent<GunController>().name;
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
