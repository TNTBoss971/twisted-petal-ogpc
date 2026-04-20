using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSceneChangeButton : MonoBehaviour
{
    private Button button;
    public DataPersistanceManager dataManager;
    public string destination;

    private GameObject fadeBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        fadeBox = GameObject.Find("FadeBox");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void TaskOnClick()
    {
        // When pressed, saves the game and changes to the selected scene
        dataManager.SaveGame();
        fadeBox.GetComponent<Animator>().speed = 4f;
        fadeBox.GetComponent<Animator>().Play("FadeOut");
        Invoke(nameof(FadeOut), 0.25f);
    }
    void FadeOut()
    {
        SceneManager.LoadScene(destination);
    }
}
