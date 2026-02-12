using UnityEditor.Overlays;
using UnityEngine;

public class LevelSummary : MonoBehaviour
{
    public int summaryID;
    private JournalManager journalManager;
    private string summary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        journalManager = FindAnyObjectByType<JournalManager>();
        summary = journalManager.GetComponent<DataManagement>().levelSummaries[summaryID];
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = summary;
    }
}
