using UnityEngine;
using System.Collections.Generic;


public class CutsceneManager : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueSprite portrait;
    public List<string> cutsceneDialogueLines; // every dialogue line
    public List<Sprite> cutsceneDialoguePortraits; // every dialogue portrait
    public List<int> cutsceneDecisions; // every cutscene decision
    // a dictionary containing the start point for dialogue in a given cutscene
    Dictionary<int, int> cutsceneLinesStart = new Dictionary<int, int>
    {
        {1, 0},
        {2, 3},
        {3, 5},
        {4, 8},
        {5, 12},
        {6, 15},
        {7, 18}
    };
    // a dictionary containing the end point for dialogue in a given cutscene
    Dictionary<int, int> cutsceneLinesEnd = new Dictionary<int, int>
    {
        {1, 3},
        {2, 5},
        {3, 8},
        {4, 12},
        {5, 15},
        {6, 18},
        {7, 21}
    };
    private DataManagement saveData;
    private bool linesLoaded;
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
            dialogue.cutsceneDialogueCount = cutsceneLinesStart[saveData.levelsBeaten];
            // take text from this script into the dialogue script
            for (int i = cutsceneLinesStart[saveData.levelsBeaten]; i < cutsceneLinesEnd[saveData.levelsBeaten]; i++)
            {
                dialogue.dialogueLines.Add(cutsceneDialogueLines[i]);
            }
            // take sprites from this script into the dialogue portrait script
            for (int i = cutsceneLinesStart[saveData.levelsBeaten]; i < cutsceneLinesEnd[saveData.levelsBeaten]; i++)
            {
                portrait.dialogueSprites.Add(cutsceneDialoguePortraits[i]);
            }
            linesLoaded = true;
        }
    }
}
