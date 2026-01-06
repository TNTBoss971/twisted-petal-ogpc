using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDecisionButton : MonoBehaviour
{
    public int buttonID;
    public CutsceneManager cutscenes;
    public Button button;
    private CanvasGroup canvasGroup;
    public bool dialogueLocked;
    private DataManagement saveData;
    public string buttonText;
    public int scenarioID;
    public List<GameObject> itemsIndex;
    public bool decisionAllowed;
    public GameObject otherButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        dialogueLocked = false;
        button.onClick.AddListener(TaskOnClick);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        decisionAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        // disable this button if another one is also disabled
        if (otherButton.GetComponent<CanvasGroup>().interactable == false)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
        
        if (dialogueLocked == false)
        {
            // checks if there's a decision
            if (cutscenes.cutsceneDecisions[Dialogue.currentLine] != 0)
            {
                if (decisionAllowed == true)
                {
                    canvasGroup.alpha = 1f;
                    canvasGroup.interactable = true;
                    dialogueLocked = true;
                    // checks the cutscenemanager list to see which decision we're doing
                    switch (cutscenes.cutsceneDecisions[Dialogue.currentLine])
                    {
                        case 1:
                            scenarioID = 1;
                            if (buttonID == 1)
                            {
                                buttonText = "Yes please!";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Keep it";
                            }
                            break;
                        case 2:
                            Debug.Log("test");
                            break;
                        case 3:
                            Debug.Log("test");
                            break;
                    }
                }

            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
            }
        }
        
    }

    void GiveItem(GameObject itemGiven)
    {
        saveData.ownedItems.Add(itemGiven);
    }

    void TaskOnClick()
    {
        switch (scenarioID)
        {
            case 1:
                if (buttonID == 1)
                {
                    GiveItem(itemsIndex[4]);
                }
                if (buttonID == 2)
                {

                }
                break;
        }
        decisionAllowed = false;
        dialogueLocked = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }
}
