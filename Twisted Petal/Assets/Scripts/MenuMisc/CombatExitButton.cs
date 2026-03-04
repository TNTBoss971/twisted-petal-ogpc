using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatExitButton : MonoBehaviour
{
    private GameManagement gameManager;
    private DataManagement saveData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        saveData = gameManager.GetComponent<DataManagement>();
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
        saveData.itemsLootedOverall += GameManagement.itemsLooted;
        saveData.enemiesBeaten = GameManagement.enemiesBeaten;
        saveData.enemiesBeatenOverall += GameManagement.enemiesBeaten;
        saveData.itemsLooted = GameManagement.itemsLooted;
        gameManager.dataManager.SaveGame();
        gameManager.paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("WorldMap");
    }
}
