using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JournalContinueButton : MonoBehaviour
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
