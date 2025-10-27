using UnityEngine;
using UnityEngine.UI;

public class TestSaveButton : MonoBehaviour
{
    public Button button;
    public DataPersistanceManager dataManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Lets the button know when it is pressed
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        // When pressed, saves the game
        dataManager.SaveGame();
    }
}
