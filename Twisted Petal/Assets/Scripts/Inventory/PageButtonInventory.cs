using UnityEngine;
using UnityEngine.UI;

public class PageButtonInventory : MonoBehaviour
{
    private Button button;
    private InventoryManager inventoryManager;
    public static int page;
    public int buttonID;
    public static int maxPages;
    private bool canKeepGoing;
    private int tempPageTestVar;
    public bool disabled;
    private int tempMaxPages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        button.onClick.AddListener(TaskOnClick);
        disabled = false;
        CalculateMaxPages();
    }

    // Update is called once per frame
    void Update()
    {
        if (page >= maxPages)
        {
            if (buttonID == 1)
            {
                disabled = true;
            }
        }
        else
        {
            if (buttonID == 1)
            {
                disabled = false;
            }
        }

        if (page <= 0)
        {
            if (buttonID != 1)
            {
                disabled = true;
            }
        }
        else
        {
            if (buttonID != 1)
            {
                disabled = false;
            }
        }

        if (disabled == true)
        {
            gameObject.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    
    void TaskOnClick()
    {
        if (buttonID == 1)
        {
            if (disabled == false)
            {
                page += 1;
                inventoryManager.GenerateButtons(page * 20);
            }
        }
        else
        {
            if (disabled == false)
            {
                page -= 1;
                inventoryManager.GenerateButtons(page * 20);
            }
        }
        
    }

    public void CalculateMaxPages()
    {
        if (buttonID == 1)
        {
            tempMaxPages = 0;
            tempPageTestVar = 0;
            canKeepGoing = true;
            while (canKeepGoing == true)
            {
                tempPageTestVar += 1;
                if (inventoryManager.ownedItems.Count > ((tempPageTestVar) * 20))
                {
                    tempMaxPages += 1;
                }
                else
                {
                    canKeepGoing = false;
                }
            }
            if (page > tempMaxPages)
            {
                page = tempMaxPages;
                inventoryManager.GenerateButtons(page * 20);
            }
            maxPages = tempMaxPages;
        }
    }
}
