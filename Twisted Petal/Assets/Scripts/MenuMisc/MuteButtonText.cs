using UnityEngine;

public class MuteButtonText : MonoBehaviour
{
    private DataManagement saveData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = GetComponentInParent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.soundsMuted == true)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Unmute SFX";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Mute SFX";
        }
    }
}
