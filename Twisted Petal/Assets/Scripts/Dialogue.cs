using System.Collections.Generic;
using System.Collections;
using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        talking = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = displayedDialogue;
        if (Input.GetMouseButtonDown(0))
        {
            if (talking == true)
            {
                displayedDialogue = dialogueLines[currentLine];
                talking = false;
            }
            else
            {
                if (currentLine < dialogueLines.Count - 1)
                {
                    currentCharacter = 0;
                    talking = false;
                    currentLine += 1;
                    displayedDialogue = "";
                    talking = true;
                }
            }
        }
        if (talking == true)
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
    }
}
