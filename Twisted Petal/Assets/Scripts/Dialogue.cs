using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Dialogue : MonoBehaviour
{
    private int dialogueLength; // the amount of characters in the current dialogue line
    private string displayedDialogue; // the text currently displayed in the dialogue box
    public List<string> dialogueLines; // every line of dialogue for this dialogue box
    private bool talking = false; // whether or not dialogue is being printed
    public static int currentLine = 0; // the current line of dialogue being printed
    private float dialogueDelay = 0f; // used to keep track of when new characters can be printed
    private int currentCharacter = 0; // the id of the current character being printed
    public static float textSpeed = 0.05f; // The delay in between characters getting printed (in seconds)
    public bool dialogueLocked;
    public DataPersistanceManager dataManager;
    public int cutsceneDialogueCount; // current dialogue line in the context of every dialogue line ingame

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        talking = true;
        currentLine = 0;
        dialogueDelay = 0f;
        currentCharacter = 0;
        textSpeed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = displayedDialogue;
        if (Input.GetMouseButtonDown(0))
        {
            if (talking == true)
            {
                // skip to the end of the line if the player clicks
                displayedDialogue = dialogueLines[currentLine];
                talking = false;
            }
            else
            {
                // checking if there is a decision ongoing
                if (dialogueLocked == false)
                {
                    // checking if the cutscene is out of dialogue
                    if (currentLine < dialogueLines.Count - 1)
                    {
                        currentCharacter = 0;
                        talking = false;
                        currentLine += 1;
                        cutsceneDialogueCount += 1;
                        displayedDialogue = "";
                        talking = true;
                    }
                    else
                    {
                        dataManager.SaveGame();
                        SceneManager.LoadScene("CombatResolution");
                    }
                }
            }
        }
        if (talking == true)
        {
            // print text gradually according to text speed
            try
            {
                dialogueLength = dialogueLines[currentLine].Length;
                if (dialogueDelay <= Time.time)
                {
                    displayedDialogue += dialogueLines[currentLine][currentCharacter];
                    currentCharacter++;
                    dialogueDelay = Time.time + textSpeed;
                }
                if (displayedDialogue.Length == dialogueLength)
                {
                    currentCharacter = 0;
                    talking = false;
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
    }
}
