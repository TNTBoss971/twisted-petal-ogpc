using UnityEngine;

public class SummariesScrollbar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSummary.levelSummariesHidden == true)
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
