using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatExitButton : MonoBehaviour
{
    private GameManagement gameManager;
    private DataManagement saveData;
    private GameObject fadeBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        saveData = gameManager.GetComponent<DataManagement>();
        fadeBox = GameObject.Find("FadeBox");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.paused == true)
        {
            this.GetComponent<CanvasGroup>().alpha = 1;
            this.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
            this.GetComponent<CanvasGroup>().interactable = false;
        }
    }

    void TaskOnClick()
    {
        saveData.itemsLootedOverall += gameManager.itemsLooted;
        saveData.enemiesBeaten = gameManager.enemiesBeaten;
        saveData.enemiesBeatenOverall += gameManager.enemiesBeaten;
        saveData.itemsLooted = gameManager.itemsLooted;
        gameManager.dataManager.SaveGame();
        gameManager.paused = false;
        Time.timeScale = 1f;
        fadeBox.GetComponent<Animator>().Play("FadeOut");
        Invoke(nameof(LoadSceneDelayed), 1f);
    }

    void LoadSceneDelayed()
    {
        SceneManager.LoadScene("WorldMap");
    }
}
