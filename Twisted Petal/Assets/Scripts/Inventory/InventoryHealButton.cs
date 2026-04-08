using UnityEngine;
using UnityEngine.UI;

public class InventoryHealButton : MonoBehaviour
{
    private Button button;
    public InventoryManager inventoryManager;
    private DataManagement saveData;
    public int supplyHealthValue; // how much hp supplies give
    public GameObject healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        saveData = inventoryManager.GetComponent<DataManagement>();
        if (saveData.currentHealth > saveData.maxHealth)
        {
            saveData.currentHealth = saveData.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.GetComponent<TMPro.TextMeshProUGUI>().text = "Health: " + saveData.currentHealth + "/" + saveData.maxHealth;
    }
    
    void TaskOnClick()
    {
        // When pressed, scraps selected items
        if (saveData.supplies >= 1 && saveData.currentHealth < saveData.maxHealth)
        {
            if ((saveData.currentHealth + supplyHealthValue) > saveData.maxHealth)
            {
                saveData.currentHealth = saveData.maxHealth;
            }
            else
            {
                saveData.currentHealth += supplyHealthValue;
            }
            saveData.supplies -= 1;
        }
    }
}
