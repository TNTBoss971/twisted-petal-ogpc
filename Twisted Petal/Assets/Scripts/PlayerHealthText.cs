using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthText : MonoBehaviour
{

    public GameManagement gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Health: " + gameManager.playerHealth;
    }
}