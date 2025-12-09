using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private int dialogueLength;
    private string displayedDialogue;
    public List<string> dialogueLines;
    private bool talking = false;
    private int currentLine = 0;
    private float dialogueDelay = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayText();
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
                    currentLine += 1;
                    displayedDialogue = "";
                    DisplayText();
                }
            }
        }
    }
    
    public IEnumerator DisplayCharacter(char character)
    {
        displayedDialogue += character;
    }


    public void DisplayText()
    {
        talking = true;
        dialogueLength = dialogueLines[currentLine].Length;
        for (int i = 0; i < dialogueLength; i++)
        {
            bool characterPrinted = false;
            dialogueDelay = Time.time + 0.3f;
            DisplayCharacter(dialogueLines[currentLine][i]);
            StartCoroutine("DisplayDelayFunction", dialogueDelay * i);
            while (characterPrinted != true)
            {
                if (dialogueDelay <= Time.time)
                {
                    characterPrinted = true;
                }
            }
        }
        talking = false;
    }
}
