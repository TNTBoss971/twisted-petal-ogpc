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
    private int currentCharacter = 0;
    public static float textSpeed = 0.05f;
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
