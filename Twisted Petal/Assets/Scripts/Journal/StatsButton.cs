using UnityEngine;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour
{
    private Button button;
    public int buttonID;
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
        if (buttonID == 1)
        {
            JournalStats.statsHidden = false;
            WeaponsFound.weaponsFoundHidden = true;
            LevelSummary.levelSummariesHidden = true;
        }
        if (buttonID == 2)
        {
            JournalStats.statsHidden = true;
            WeaponsFound.weaponsFoundHidden = false;
            LevelSummary.levelSummariesHidden = true;
        }
        if (buttonID == 3)
        {
            JournalStats.statsHidden = true;
            WeaponsFound.weaponsFoundHidden = true;
            LevelSummary.levelSummariesHidden = false;
        }
        
    }
}
