using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestResetButton : MonoBehaviour
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
        // When pressed, resets the testing variables and pushes the default values to the save file

        TestText1.testingVariableOne = 0;
        TestText2.testingVariableTwo = 0;
        dataManager.SaveGame();
    }
}
