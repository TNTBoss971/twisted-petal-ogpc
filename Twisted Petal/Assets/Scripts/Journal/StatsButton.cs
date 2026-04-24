using UnityEngine;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour
{
    private Button button;
    public int buttonID;
    public Transform frontTransform;
    public Transform backTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        JournalStats.statsHidden = false;
        WeaponsFound.weaponsFoundHidden = true;
        LevelSummary.levelSummariesHidden = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonID == 1)
        {
            if (JournalStats.statsHidden)
            {
                transform.parent = backTransform;
            }
            else
            {
                transform.parent = frontTransform;
            }
        }
        if (buttonID == 2)
        {
            if (WeaponsFound.weaponsFoundHidden)
            {
                transform.parent = backTransform;
            }
            else
            {
                transform.parent = frontTransform;
            }
        }
        if (buttonID == 3)
        {
            if (LevelSummary.levelSummariesHidden)
            {
                transform.parent = backTransform;
            }
            else
            {
                transform.parent = frontTransform;
            }
        }
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
