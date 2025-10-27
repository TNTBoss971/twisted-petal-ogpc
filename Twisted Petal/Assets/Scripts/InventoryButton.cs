using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public static int buttonNumber = 0;
    private int buttonID = 0;
    public Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonNumber += 1;
        buttonID = buttonNumber;
        Debug.Log(buttonID);
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
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
