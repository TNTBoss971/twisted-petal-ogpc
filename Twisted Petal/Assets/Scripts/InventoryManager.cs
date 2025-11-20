using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static List<GameObject> selectedItems = new List<GameObject>();
    public static List<int> selectedIDs = new List<int>();
    public static List<int> selectedDescriptions = new List<int>();
    public List<GameObject> ownedItems;
    private DataManagement saveData;
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
