using UnityEngine;
using UnityEngine.UI;

public class TextSpeedButton : MonoBehaviour
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
            Dialogue.textSpeed = 0.1f;
        }
        else if (buttonID == 2)
        {
            Dialogue.textSpeed = 0.05f;
        }
        else
        {
            Dialogue.textSpeed = 0.005f;
        }
    }
}
