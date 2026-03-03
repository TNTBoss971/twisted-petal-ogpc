using UnityEngine;

public class EscapingPopup : MonoBehaviour
{
    private GameManagement gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.escaping == true)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Exiting level...";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Hold Esape to Exit";
        }
    }
}
