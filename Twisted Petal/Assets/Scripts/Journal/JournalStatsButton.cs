using UnityEngine;
using UnityEngine.UI;

public class JournalStatsButton : MonoBehaviour
{
    public Button button;
    public int buttonID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        if (buttonID == 1)
        {
            JournalStats.currentStats = true;
        }
        else
        {
            JournalStats.currentStats = false;
        }
    }
}
