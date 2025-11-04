using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // We should probably change this to a dictionary or something instead of if statement spam
        // Maybe even classes or smth idk.

        if (InventoryManager.selectedItem == 0)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "No Item Selected";
        }
        else if (InventoryManager.selectedItem == 1)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Red Item - It's red!";
        }
        else if (InventoryManager.selectedItem == 2)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Greeb Item - It's green!";
        }
        else if (InventoryManager.selectedItem == 3)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Blue Item - It's blue!";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Unknown Item - It's unknown!";
        }
    }
}
