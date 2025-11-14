using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public int buttonID;
    public Button button;
    private bool selected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // Changes item to green if it's selected.
        if (InventoryManager.selectedItems.Contains(buttonID))
        {
            selected = true;
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            selected = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    void TaskOnClick()
    {
        if (selected == true)
        {
            InventoryManager.selectedItems.Remove(buttonID);
        }
        else
        {
            if (InventoryManager.selectedItems.Count < 3)
            {
                InventoryManager.selectedItems.Add(buttonID);
            }
        }
    }
}
