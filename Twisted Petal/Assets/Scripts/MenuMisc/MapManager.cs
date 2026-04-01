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
    public List<float> posX;
    public List<float> posY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showError == true)
        {
            if (errorTimer <= Time.time)
            {
                showError = false;
            }
        }
        mapPosition = saveData.levelsBeaten + 1;
        if (mapPosition > 7)
        {
            mapPosition = 7;
        }

        playerMapIcon.transform.position = new Vector2(posX[mapPosition - 1], posY[mapPosition - 1]);

        // Pressing enter on the map takes you into a level
        if (Input.GetKey("return"))
        {
            if (saveData.selectedItems.Count <= 0)
            {
                showError = true;
                errorTimer = Time.time + 1.5f;
            }
            else
            {
                SceneManager.LoadScene("Combat");
            }
        }
    }
}
