using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSprite : MonoBehaviour
{
    public List<Sprite> dialogueSprites;
    private int spritesLength;
    private int currentSprite;
    private Image portraitBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portraitBox = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSprite = Dialogue.currentLine;
        portraitBox.sprite = dialogueSprites[currentSprite];
    }
}
