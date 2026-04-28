using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayButton : MonoBehaviour
{
    public Button button;
    public bool hasSaveFile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Lets the button know when it is pressed
        button.onClick.AddListener(TaskOnClick);
        if (File.Exists(Application.persistentDataPath + "/saved_data.json"))
        {
            hasSaveFile = true;
        }
        else
        {
            hasSaveFile = false;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        // When pressed, takes the player to the world map
        // Might change to another scene later
        if (hasSaveFile == true)
        {
            SceneManager.LoadScene("WorldMap");
        }
    }
}
