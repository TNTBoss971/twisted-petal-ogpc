using UnityEngine;
using UnityEngine.UI;

public class TestButton1 : MonoBehaviour
{
    public Button button;
    
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
        // When pressed, changes 1st variable by 1
        TestText1.testingVariableOne += 1;
    }
}
