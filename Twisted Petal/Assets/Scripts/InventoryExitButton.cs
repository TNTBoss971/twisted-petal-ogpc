using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryExitButton : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void TaskOnClick()
    {
        // When pressed, exits the inventory to the map screen
        SceneManager.LoadScene("WorldMap");
    }
}
