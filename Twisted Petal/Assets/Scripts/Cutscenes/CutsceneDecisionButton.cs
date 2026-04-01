using System;
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
    private bool actionPerformed; // has the action already been performed?
    private CutsceneManager custceneManager;
    public enum decisionsMade
    {
        didntTakeMoreSupplies, // took only what you needed
        tookMoreSupplies // took everything
    }

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
        actionPerformed = false;
        custceneManager = FindAnyObjectByType<CutsceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // checks if there's a decision
            if (cutscenes.currentCutscene.decisions[dialogue.cutsceneDialogueCount] != 0)
            {
                if (decisionAllowed == true)
                {
                    canvasGroup.alpha = 1f;
                    canvasGroup.interactable = true;
                    // checks the cutscenemanager list to see which decision we're doing
                    // then makes each button do something different depending on which
                    // decision we got
                    switch (cutscenes.currentCutscene.decisions[dialogue.cutsceneDialogueCount])
                    {
                        case CutsceneData.decisionType.SupplyCache:
                            scenarioID = 1;
                            if (buttonID == 1)
                            {
                                
                            }
                            if (buttonID == 2)
                            {
                                
                            }
                            break;
                        case CutsceneData.decisionType.SupplyConflict:
                            scenarioID = 2;
                            if (buttonID == 1)
                            {
                                buttonText = "Take only what you need";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Take everything";
                            }
                            break;
                        case CutsceneData.decisionType.ThinkBack:
                            scenarioID = 3;
                            if (buttonID == 1)
                            {
                                
                            }
                            if (buttonID == 2)
                            {
                                
                            }
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
        catch (ArgumentOutOfRangeException)
        {
            
        }
        catch (NullReferenceException)
        {
            
        }
        if (scenarioID == 1)
        {
            decisionAllowed = false;
            dialogue.dialogueLocked = false;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            if (buttonID == 1)
            {
                if (actionPerformed == false)
                {
                    GiveSupplies(3);
                }
            }
        }
        if (scenarioID == 3)
        {
            decisionAllowed = false;
            dialogue.dialogueLocked = false;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            if (buttonID == 1)
            {
                if (saveData.choicesMade.Contains(decisionsMade.tookMoreSupplies))
                {
                    if (actionPerformed == false)
                    {
                        dialogue.dialogueLines.Clear();
                        for (int i = 0; i < Dialogue.currentLine; i++)
                        {
                            dialogue.dialogueLines.Add("");
                        }
                        for (int i = 0; i < cutscenes.currentCutscene.altLinesOne.Count; i++)
                        {
                            dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesOne[i]);
                        }
                        actionPerformed = true;
                    }
                }
            }
        }
    }

    // This function allows for buttons to add
    // items to the player's inventory
    void GiveItem(GameObject itemGiven)
    {
        saveData.ownedItems.Add(itemGiven);
    }

    void GiveSupplies(int amount)
    {
        custceneManager.GetComponent<DataManagement>().supplies += amount;
        actionPerformed = true;
    }

    void TaskOnClick()
    {
        // find out which scenario we're doing
        // and also which button we clicked
        // and acts accordingly
        switch (cutscenes.currentCutscene.decisions[dialogue.cutsceneDialogueCount])
        {
            case CutsceneData.decisionType.SupplyCache:
                if (buttonID == 1)
                {
                    
                }
                if (buttonID == 2)
                {

                }
                break;
            case CutsceneData.decisionType.SupplyConflict:
                if (buttonID == 1)
                {
                    GiveSupplies(2);
                    saveData.choicesMade.Add(decisionsMade.didntTakeMoreSupplies);
                }
                if (buttonID == 2)
                {
                    dialogue.dialogueLines.Clear();
                    GiveSupplies(8);
                    for (int i = 0; i < Dialogue.currentLine + 1; i++)
                    {
                        dialogue.dialogueLines.Add("");
                    }
                    for (int i = 0; i < cutscenes.currentCutscene.altLinesOne.Count; i++)
                    {
                        dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesOne[i]);
                    }
                    saveData.choicesMade.Add(decisionsMade.tookMoreSupplies);
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
