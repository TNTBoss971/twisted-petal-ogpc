using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class SummariesScrollbar : MonoBehaviour
{
    private Scrollbar scrollbar;
    public int barValue;
    private JournalManager journalManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.onValueChanged.AddListener(OnScroll);
        journalManager = FindAnyObjectByType<JournalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSummary.levelSummariesHidden == true || journalManager.GetComponent<DataManagement>().levelSummaries.Count <= 2)
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void OnScroll(float value)
    {
        barValue = (int)(Math.Round(value, 1) * 20);
    }
}
