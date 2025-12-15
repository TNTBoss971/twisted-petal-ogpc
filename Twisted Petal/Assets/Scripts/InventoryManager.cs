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
    public GameObject buttonPrefab;
    public Canvas canvas;
    private DataManagement saveData;
    // This list is just so other objects in the scene can acess ownedItems without having to mess with savedata
    public static List<GameObject> inventoryWeaponTypes;

    public SlotDisplayLogic[] displays;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();

        int row = 0;
        int col = 0;

        for (int i = 0; i < ownedItems.Count; i++)
        {
            GameObject clone = Instantiate(buttonPrefab, transform.position, transform.rotation);
            clone.transform.parent = canvas.transform;
            clone.GetComponent<InventoryButton>().buttonID = i;


            clone.transform.position = new Vector2(row * 175 + 100f, col * -175 + 750);
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
        inventoryWeaponTypes = ownedItems;

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
