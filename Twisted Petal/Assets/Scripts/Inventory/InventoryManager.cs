using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;

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
    public GameObject buttonPrefab;
    public Canvas canvas;
    public SlotDisplayLogic[] displays;
    public int rarityChance;
    public GameObject supplyCounter;
    private int buttonCount; // how many buttons get made
    public List<GameObject> currentButtons; // list of buttons in the scene
    private int tempButtonsCount; // used in a loop that deletes buttons
    public GameObject pageLabel; // label used to track the current page of the inventory.

    private Transform weaponsParent;

    private GameObject fadeBox;
    private Sprite emptyImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponsParent = GameObject.Find("WeaponParent").transform;
        fadeBox = GameObject.Find("FadeBox");
        fadeBox.GetComponent<Animator>().Play("FadeIn");

        inventoryWeaponTypes = weaponTypes;
        saveData = this.GetComponent<DataManagement>();
        selectedItems = saveData.selectedItems;
        selectedIDs = saveData.selectedButtonIDs;
        emptyImage = displays[0].displayImage;
        if (saveData.ownedItems.Count <= 0)
        {
            ownedItems = startingWeapons;
            saveData.ownedItems = ownedItems;
        }
        else
        {
            ownedItems = saveData.ownedItems;
        }

        // create the buttons that display the weapons
        int row = 0;
        int col = 0;

        if (ownedItems.Count > 20)
        {
            buttonCount = 20;
        }
        else
        {
            buttonCount = ownedItems.Count;
        }

        for (int i = 0; i < buttonCount; i++)
        {
            // clone the prefab
            GameObject clone = Instantiate(buttonPrefab, transform.position, transform.rotation);
            // set the parent
            clone.transform.SetParent(weaponsParent, false);
            // set the id
            clone.GetComponent<InventoryButton>().buttonID = i;
            // set the item stored
            clone.GetComponent<InventoryButton>().itemStored = ownedItems[i];
            // set the position
            clone.transform.position = new Vector2(row * 160 + 100f, col * -175 + 890);

            row++;
            if (row > 9)
            {
                col++;
                row = 0;
            }
            currentButtons.Add(clone);
        }

    }

    public void GenerateButtons(int amountOfButtons)
    {
        if ((ownedItems.Count - amountOfButtons) > 0)
        {
            tempButtonsCount = currentButtons.Count;
            for (int i = 0; i < tempButtonsCount; i++)
            {
                currentButtons[0].GetComponent<InventoryButton>().DeleteSelf();
                currentButtons.Remove(currentButtons[0]);
            }

            int row = 0;
            int col = 0;

            for (int i = amountOfButtons; i < (amountOfButtons + 20); i++)
            {
                try
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
                    clone.transform.position = new Vector2(row * 160 + 100f, col * -175 + 890);

                    row++;
                    if (row > 9)
                    {
                        col++;
                        row = 0;
                    }
                    currentButtons.Add(clone);
                }
                catch(ArgumentOutOfRangeException)
                {
                    
                }
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
                displays[i].displayImage = emptyImage;
                displays[i].displayName = "Empty";
                displays[i].displayDiscription = "Useless. Probably should put a weapon here.";
            }
        }

        supplyCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Supplies: " + saveData.supplies;

        pageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = "Page: " + (PageButtonInventory.page + 1) + "/" + (PageButtonInventory.maxPages + 1);
    }
}
