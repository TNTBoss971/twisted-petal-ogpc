using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static int mapPosition = 1;
    private DataManagement saveData;
    public DataPersistanceManager dataManager;
    public bool showError = false;
    private float errorTimer;
    public List<GameObject> startingWeapons;
    public GameObject playerMapIcon;
    public GameObject worldMap;
    public List<Sprite> worldMapSprites;
    public List<Vector2> pos;
    public GameObject mapErrorText;

    private GameObject fadeBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
        playerMapIcon.SetActive(false);
        fadeBox = GameObject.Find("FadeBox");
        fadeBox.GetComponent<Animator>().Play("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (showError == true)
        {
            if (errorTimer <= Time.time)
            {
                mapErrorText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                showError = false;
            }
        }
        mapPosition = saveData.levelsBeaten + 1;
        if (mapPosition > 15)
        {
            mapPosition = 15;
        }

        worldMap.GetComponent<SpriteRenderer>().sprite = worldMapSprites[saveData.levelsBeaten];
        playerMapIcon.transform.localPosition = pos[mapPosition - 1];

        // Pressing enter on the map takes you into a level
        if (Input.GetKey("return"))
        {
            if (saveData.selectedItems.Count <= 0)
            {
                showError = true;
                mapErrorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Equip atleast 1 weapon from the inventory before entering a level.";
                errorTimer = Time.time + 1.5f;
            }
            else
            {
                playerMapIcon.SetActive(true);
                fadeBox.GetComponent<Animator>().Play("FadeOut");
                Invoke(nameof(EnterCombat), 1.4f);
            }
        }
    }

    public void EnterCombat()
    {
        SceneManager.LoadScene("Combat");
    }
}
