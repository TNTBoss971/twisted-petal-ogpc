using UnityEngine;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    public int summaryID;
    private JournalManager journalManager;
    private string summary;
    public static bool levelSummariesHidden;
    private bool loopDone;
    private SummariesScrollbar scrollbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelSummariesHidden = true;
        journalManager = FindAnyObjectByType<JournalManager>();
        loopDone = false;
        scrollbar = FindAnyObjectByType<SummariesScrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loopDone == false)
        {
            if (journalManager.GetComponent<DataManagement>().levelSummaries.Count > summaryID)
            {
                summary = journalManager.GetComponent<DataManagement>().levelSummaries[summaryID];
            }
            loopDone = true;
        }
        if (levelSummariesHidden == true)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = summary;
            if (journalManager.GetComponent<DataManagement>().levelSummaries.Count > 7)
            {
                summary = journalManager.GetComponent<DataManagement>().levelSummaries[summaryID + scrollbar.barValue];
            }
        }
    }
}
