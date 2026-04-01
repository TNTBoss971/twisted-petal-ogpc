using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Button button;
    private DataManagement saveData;
    public DataPersistanceManager dataManager;
    public GameObject muteButtonText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.soundsMuted == true)
        {
            muteButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = "Unmute SFX";
        }
        else
        {
            muteButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = "Mute SFX";
        }
    }
    
    void TaskOnClick()
    {
        // When pressed, mutes or unmutes sound effects
        if (saveData.soundsMuted == true)
        {
            saveData.soundsMuted = false;
        }
        else
        {
            saveData.soundsMuted = true;
        }
        dataManager.SaveGame();
    }
}
