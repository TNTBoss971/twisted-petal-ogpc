using System;
using System.Collections.Generic;
using System.IO;
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
    public GameObject decisionText; // the actual text object to display the text
    public int scenarioID; // current decision scenario
    public List<GameObject> itemsIndex; // every item in the game
    public bool decisionAllowed; // are we worrying about decisions right now?
    public Dialogue dialogue; // the dialogue box
    private bool actionPerformed; // has the action already been performed?
    private CutsceneManager custceneManager;
    public enum decisionsMade
    {
        didntTakeMoreSupplies, // took only what you needed
        tookMoreSupplies, // took everything
        gaveCharity, // gave out supplies
        apologized, // didn't have enough supplies to give
        didntGiveCharity, // didn't give out supplies
        gaveToBarrenCache, // contributed supplies to the barren cache
        lootedBarrenCache, // took everything from the barren supply cache
        tookTurret, // stole the laser turret from the safe area
        repairedWall // repaired the wall at the gas station
    }
    public int moralityNumber; // number to track morality
    private string path; // path for saving files
    private string journalContent; // content in the journal.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        dialogue.dialogueLocked = false;
        button.onClick.AddListener(TaskOnClick);
        decisionAllowed = true;
        actionPerformed = false;
        custceneManager = FindAnyObjectByType<CutsceneManager>();
        moralityNumber = 0;
        if (saveData.choicesMade.Contains(decisionsMade.tookMoreSupplies))
        {
            moralityNumber -= 2;
        }
        if (saveData.choicesMade.Contains(decisionsMade.didntGiveCharity))
        {
            moralityNumber -= 1;
        }
        if (saveData.choicesMade.Contains(decisionsMade.gaveCharity))
        {
            moralityNumber += 2;
        }
        if (saveData.choicesMade.Contains(decisionsMade.didntTakeMoreSupplies))
        {
            moralityNumber += 1;
        }
        if (saveData.choicesMade.Contains(decisionsMade.gaveToBarrenCache))
        {
            moralityNumber += 2;
        }
        if (saveData.choicesMade.Contains(decisionsMade.lootedBarrenCache))
        {
            moralityNumber -= 1;
        }
        if (saveData.choicesMade.Contains(decisionsMade.tookTurret))
        {
            moralityNumber -= 3;
        }
        if (saveData.choicesMade.Contains(decisionsMade.repairedWall))
        {
            moralityNumber += 3;
        }
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
                        case CutsceneData.decisionType.Charity:
                            scenarioID = 4;
                            if (buttonID == 1)
                            {
                                if (saveData.supplies >= 3)
                                {
                                    buttonText = "Give them supplies";
                                }
                                else
                                {
                                    buttonText = "Explain you don't have enough";
                                }
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Don't give them supplies";
                            }
                            break;
                        case CutsceneData.decisionType.Market:
                            scenarioID = 4;
                            if (buttonID == 1)
                            {
                                buttonText = "Buy it";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Don't buy it";
                            }
                            break;
                        case CutsceneData.decisionType.Ponder:
                            scenarioID = 5;
                            if (buttonID == 1)
                            {
                                
                            }
                            if (buttonID == 2)
                            {
                                
                            }
                            break;
                        case CutsceneData.decisionType.BarrenCache:
                            scenarioID = 6;
                            if (buttonID == 1)
                            {
                                buttonText = "Contribute 3 supplies";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Take what's left.";
                            }
                            break;
                        case CutsceneData.decisionType.LaserTurret:
                            scenarioID = 7;
                            if (buttonID == 1)
                            {
                                buttonText = "Leave it.";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Take it.";
                            }
                            break;
                        case CutsceneData.decisionType.WallRepair:
                            scenarioID = 8;
                            if (buttonID == 1)
                            {
                                buttonText = "Repair the wall.";
                            }
                            if (buttonID == 2)
                            {
                                buttonText = "Don't repair the wall.";
                            }
                            break;
                        case CutsceneData.decisionType.TheEnding:
                            scenarioID = 9;
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
        if (scenarioID == 5)
        {
            decisionAllowed = false;
            dialogue.dialogueLocked = false;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            if (buttonID == 1)
            {
                if (moralityNumber < 0)
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
                if (moralityNumber == 0)
                {
                    if (actionPerformed == false)
                    {
                        dialogue.dialogueLines.Clear();
                        for (int i = 0; i < Dialogue.currentLine; i++)
                        {
                            dialogue.dialogueLines.Add("");
                        }
                        for (int i = 0; i < cutscenes.currentCutscene.altLinesTwo.Count; i++)
                        {
                            dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesTwo[i]);
                        }
                        actionPerformed = true;
                    }
                }
            }
            
        }
        if (scenarioID == 9)
        {
            decisionAllowed = false;
            dialogue.dialogueLocked = false;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            if (buttonID == 1)
            {
                if (moralityNumber < 0)
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
                        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/my_journal.txt";

                        journalContent += "Defeated " + saveData.enemiesBeatenOverall + " Enemies \n";
                        journalContent += "Found " + saveData.itemsLootedOverall + " Items \n";
                        for (int i = 0; i < saveData.levelSummaries.Count; i++)
                        {
                            journalContent += saveData.levelSummaries[i] + "\n";
                        }
                        for (int i = 0; i < saveData.weaponsFound.Count; i++)
                        {
                            journalContent += "Discovered " + saveData.weaponsFound[i].GetComponent<GunController>().weaponName + "\n";
                        }
                        File.WriteAllText(path, journalContent);
                        Debug.Log("did it");
                        actionPerformed = true;
                    }
                }
                
                if (moralityNumber == 0)
                {
                    if (actionPerformed == false)
                    {
                        dialogue.dialogueLines.Clear();
                        for (int i = 0; i < Dialogue.currentLine; i++)
                        {
                            dialogue.dialogueLines.Add("");
                        }
                        for (int i = 0; i < cutscenes.currentCutscene.altLinesTwo.Count; i++)
                        {
                            dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesTwo[i]);
                        }
                        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/my_journal.txt";

                        journalContent += "Defeated " + saveData.enemiesBeatenOverall + " Enemies \n";
                        journalContent += "Found " + saveData.itemsLootedOverall + " Items \n";
                        for (int i = 0; i < saveData.levelSummaries.Count; i++)
                        {
                            journalContent += saveData.levelSummaries[i] + "\n";
                        }
                        for (int i = 0; i < saveData.weaponsFound.Count; i++)
                        {
                            journalContent += "Discovered " + saveData.weaponsFound[i].GetComponent<GunController>().weaponName + "\n";
                        }
                        File.WriteAllText(path, journalContent);
                        Debug.Log("did it");
                        actionPerformed = true;
                    }
                }
            }
        }
        
        decisionText.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
        Debug.Log(scenarioID);
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

    void TakeSupplies(int amount)
    {
        custceneManager.GetComponent<DataManagement>().supplies -= amount;
        actionPerformed = true;
    }

    void ReduceMaxHealth(int amount)
    {
        custceneManager.GetComponent<DataManagement>().maxHealth -= amount;
        actionPerformed = true;
    }

    void PlayAltLinesOne()
    {
        dialogue.dialogueLines.Clear();
        for (int i = 0; i < Dialogue.currentLine + 1; i++)
        {
            dialogue.dialogueLines.Add("");
        }
        for (int i = 0; i < cutscenes.currentCutscene.altLinesOne.Count; i++)
        {
            dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesOne[i]);
        }
    }

    void PlayAltLinesTwo()
    {
        dialogue.dialogueLines.Clear();
        for (int i = 0; i < Dialogue.currentLine + 1; i++)
        {
            dialogue.dialogueLines.Add("");
        }
        for (int i = 0; i < cutscenes.currentCutscene.altLinesTwo.Count; i++)
        {
            dialogue.dialogueLines.Add(cutscenes.currentCutscene.altLinesTwo[i]);
        }
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
                    GiveSupplies(8);
                    PlayAltLinesOne();
                    saveData.choicesMade.Add(decisionsMade.tookMoreSupplies);
                }
                break;
            case CutsceneData.decisionType.Charity:
                if (buttonID == 1)
                {
                    if (buttonText == "Give them supplies")
                    {
                        TakeSupplies(3);
                        saveData.choicesMade.Add(decisionsMade.gaveCharity);
                    }
                    else
                    {
                        PlayAltLinesOne();
                        saveData.choicesMade.Add(decisionsMade.apologized);
                    }
                }
                if (buttonID == 2)
                {
                    PlayAltLinesTwo();
                    saveData.choicesMade.Add(decisionsMade.didntGiveCharity);
                }
                break;
            case CutsceneData.decisionType.Market:
                if (buttonID == 1)
                {
                    if (saveData.supplies >= 5)
                    {
                        TakeSupplies(5);
                        GiveItem(itemsIndex[7]);
                    }
                    else
                    {
                        PlayAltLinesTwo();
                    }
                    
                }
                if (buttonID == 2)
                {
                    PlayAltLinesOne();
                }
                break;
            case CutsceneData.decisionType.BarrenCache:
                if (buttonID == 1)
                {
                    if (saveData.supplies >= 3)
                    {
                        TakeSupplies(3);
                        saveData.choicesMade.Add(decisionsMade.gaveToBarrenCache);
                    }
                    else
                    {
                        PlayAltLinesTwo();
                    }
                    
                }
                if (buttonID == 2)
                {
                    PlayAltLinesOne();
                    saveData.choicesMade.Add(decisionsMade.lootedBarrenCache);
                }
                break;
            case CutsceneData.decisionType.LaserTurret:
                if (buttonID == 1)
                {
                    
                }
                if (buttonID == 2)
                {
                    GiveItem(itemsIndex[3]);
                    PlayAltLinesOne();
                    saveData.choicesMade.Add(decisionsMade.tookTurret);
                }
                break;
            case CutsceneData.decisionType.WallRepair:
                if (buttonID == 1)
                {
                    ReduceMaxHealth(20);
                    saveData.choicesMade.Add(decisionsMade.repairedWall);
                }
                if (buttonID == 2)
                {
                    PlayAltLinesOne();
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
