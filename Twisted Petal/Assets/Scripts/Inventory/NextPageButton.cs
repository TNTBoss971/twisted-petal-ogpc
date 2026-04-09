using UnityEngine;
using UnityEngine.UI;

public class NextPageButton : MonoBehaviour
{
    private Button button;
    private InventoryManager inventoryManager;
    public int page;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void TaskOnClick()
    {
        page += 1;
        inventoryManager.GenerateButtons(page * 20);
    }
}
