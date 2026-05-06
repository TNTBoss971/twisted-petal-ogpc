using UnityEngine;
using System.Collections.Generic;


public class CutsceneManager : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueSprite portrait;
    public CutsceneData[] cutscenes; // a list of all the waves
    public CutsceneData currentCutscene;
    // a dictionary containing the start point for dialogue in a given cutscene
    private DataManagement saveData;
    private bool linesLoaded;
    public GameObject SkipButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        dialogue.dialogueLines.Clear();
        portrait.dialogueSprites.Clear();
        linesLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // I should be able to do this in start but for some reason saveData.levelsBeaten is always zero when
        // it's referenced in start. If you reference it in update, it's fine, and if you reference it in start()
        // in CombatResolution (journal) it also works fine BUT FOR SOME REASON HERE AND ONLY HERE
        // I HAVE TO DO THIS NONSENSE. WHYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY!?!?!?!?!?!?
        if (linesLoaded != true)
        {
            if (saveData.levelsBeaten == 0)
            {
                SkipButton.GetComponent<CanvasGroup>().alpha = 1;
                SkipButton.GetComponent<CanvasGroup>().interactable = true;
            }
            else
            {
                SkipButton.GetComponent<CanvasGroup>().alpha = 0;
                SkipButton.GetComponent<CanvasGroup>().interactable = false;
            }
            currentCutscene = cutscenes[saveData.levelsBeaten];
            for (int i = 0; i < currentCutscene.dialogueLines.Count; i++)
            {
                dialogue.dialogueLines.Add(currentCutscene.dialogueLines[i]);
                dialogue.dialogueSounds.Add(currentCutscene.dialogueSounds[i]);
            }
            for (int i = 0; i < currentCutscene.dialoguePortraits.Count; i++)
            {
                portrait.dialogueSprites.Add(currentCutscene.dialoguePortraits[i]);
            }
            linesLoaded = true;
        }
    }
}
