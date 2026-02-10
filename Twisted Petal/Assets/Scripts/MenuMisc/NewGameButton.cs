using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class NewGameButton : MonoBehaviour
{
    public Button button;
    public DataPersistanceManager dataManager;
    public List<GameObject> startingWeapons;
    
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
        // When pressed, takes the player to the world map
        //and deletes their save.
        // Might change to another scene later
        File.Delete(Application.persistentDataPath + "/saved_data.json");
        this.GetComponent<DataManagement>().ownedItems.Add(startingWeapons[0]);
        this.GetComponent<DataManagement>().ownedItems.Add(startingWeapons[1]);
        dataManager.SaveGame();
        SceneManager.LoadScene("WorldMap");
    }
}
