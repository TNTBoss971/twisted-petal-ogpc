using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSceneChangeButton : MonoBehaviour
{
    private Button button;
    public DataPersistanceManager dataManager;
    public string destination;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void TaskOnClick()
    {
        // When pressed, saves the game and changes to the selected scene
        dataManager.SaveGame();
        SceneManager.LoadScene(destination);
    }
}
