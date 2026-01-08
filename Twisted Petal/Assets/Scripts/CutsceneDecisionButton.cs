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
    private DataManagement saveData;
    public string buttonText;
    public int scenarioID;
    public List<GameObject> itemsIndex;
    public bool decisionAllowed;
    public Dialogue dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        dialogue.dialogueLocked = false;
        button.onClick.AddListener(TaskOnClick);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        decisionAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        // checks if there's a decision
        if (cutscenes.cutsceneDecisions[dialogue.cutsceneDialogueCount] != 0)
        {
            if (decisionAllowed == true)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                // checks the cutscenemanager list to see which decision we're doing
                switch (cutscenes.cutsceneDecisions[dialogue.cutsceneDialogueCount])
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
                dialogue.dialogueLocked = true;
                decisionAllowed = false;
            }
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
        // disable this button if another one is also disabled
        if (dialogue.dialogueLocked == false)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
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
                else if (buttonID == 2)
                {
                    
                }
                break;
        }
        decisionAllowed = false;
        dialogue.dialogueLocked = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }
}
