using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public int buttonID;
    public string itemDesc;
    public static string currentDesc;
    public Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the item description to that of the currently selected item.
        if (InventoryManager.selectedItem == buttonID)
        {
            currentDesc = itemDesc;
        }

        // Changes item to green if it's selected.
        // Later on we're gonna need to implement the ability to select multiple items.
        if (InventoryManager.selectedItem == buttonID)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    void TaskOnClick()
    {
        InventoryManager.selectedItem = buttonID;
    }
}
