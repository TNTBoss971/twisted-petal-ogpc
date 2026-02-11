using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDecisionButton : MonoBehaviour
{
    public int buttonID; // to differentiate each button
    public CutsceneManager cutscenes; // the cutscenemanager
    public Button button; // so the button can be selected
    private CanvasGroup canvasGroup; // canvasgroup
    private DataManagement saveData; // to access saved vars
    public string buttonText; // the button's text
    public int scenarioID; // current decision scenario
    public List<GameObject> itemsIndex; // every item in the game
    public bool decisionAllowed; // are we worrying about decisions right now?
    public Dialogue dialogue; // the dialogue box
    public List<string> alternateLines; // lines to replace current ones
    private int altLinesStart; // where do we start replacing
    private int altLinesEnd; // where do we stop replacing

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
                // then makes each button do something different depending on which
                // decision we got
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
                        scenarioID = 2;
                        altLinesStart = 0;
                        altLinesEnd = 1;
                        if (buttonID == 1)
                        {
                            buttonText = "Talk about coins";

                        }
                        if (buttonID == 2)
                        {
                            buttonText = "Talk about TV";
                        }
                        break;
                    case 3:
                        scenarioID = 3;
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
        // if dialogue is happening, turn off any buttons
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

    // This function allows for buttons to add
    // items to the player's inventory
    void GiveItem(GameObject itemGiven)
    {
        saveData.ownedItems.Add(itemGiven);
    }

    void TaskOnClick()
    {
        // find out which scenario we're doing
        // and also which button we clicked
        // and acts accordingly
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
            case 2:
                if (buttonID == 1)
                {
                    dialogue.dialogueLines.Clear();
                    for (int i = 0; i < Dialogue.currentLine + 1; i++)
                    {
                        dialogue.dialogueLines.Add("");
                    }
                    for (int i = altLinesStart; i < altLinesEnd; i++)
                    {
                        dialogue.dialogueLines.Add(alternateLines[i]);
                    }
                }
                if (buttonID == 2)
                {
                    dialogue.dialogueLines.Clear();
                    for (int i = 0; i < Dialogue.currentLine + 1; i++)
                    {
                        dialogue.dialogueLines.Add("");
                    }
                    for (int i = altLinesStart; i < altLinesEnd; i++)
                    {
                        dialogue.dialogueLines.Add(alternateLines[i]);
                    }
                }
                break;
        }
        // After you've clicked a button, no more decisions.
        decisionAllowed = false;
        dialogue.dialogueLocked = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }
}
