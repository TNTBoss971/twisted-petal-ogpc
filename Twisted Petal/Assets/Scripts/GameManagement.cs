using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    InputAction cycleAction;
    public float cycleValue;
    InputAction nextAction;
    InputAction previousAction;

    [Header("Player")]
    public float playerHealth;
    public float playerMaxHealth;

    public GameObject[] weaponButtons; // list of weapon buttons
    public GameObject[] weapons; // list of weapons
    public GameObject[] equippedWeapons; // equipped weapons
    public Transform weaponParent; // parent of the weapons
    public int numOfEquippedWeapons; // number of equipped weapons
    public int activeWeaponId;  // active weapon
    public int pastActiveWeaponId = 1; // for turning off previously active weapons

    [Header("Wave Logic")]
    public int enemyCount;
    public int enemyCountMax;
    public int waveNumber = 0;

    public float waveLength;
    public float nextWaveTime;
    public static int itemsLooted;
    public static int enemiesBeaten;

    public WaveData[] waves; // a list of all the waves
    public WaveData currentWave;
    [Header("Status Bars")]
    public BarBehavior waveProgressionBar;
    public BarBehavior healthBar;
    [Header("Save Data")]
    public DataPersistanceManager dataManager;
    public DataManagement saveData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager.LoadGame();
        itemsLooted = 0;
        enemiesBeaten = 0;
        // assign actions
        cycleAction = InputSystem.actions.FindAction("Cycle");


        StartWave();

        waveProgressionBar.maxValue = waveLength;

        healthBar.maxValue = playerMaxHealth;

        
        
        WeaponInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        cycleValue = cycleAction.ReadValue<float>(); // read the value of the scroll wheel

        WeaponManagement();


        ActiveWave();
    }
    void ActiveWave()
    {
        // continuouslly spawn enemies while wave is active
        // rarely spawn "loot" enemy
        if (enemyCount < enemyCountMax && !currentWave.isBossBattle)
        {
            // spawn enemy

            // decide enemy to spawn
            float selectedFreq = Random.Range(0.001f, 1);
        
            float[] frequencies = currentWave.enemyFrequency;
            int enemyIndex = 0;

            float totalFreq = 0;
            foreach (float freq in frequencies)
            {
                totalFreq += freq;
                // if totalFreq is withen the selected range
                if (selectedFreq <= totalFreq)
                {
                    break;
                } else {
                    enemyIndex++;
                }
            }

            GameObject clone = Instantiate(currentWave.enemiesInWave[enemyIndex], new Vector2(10, Random.Range(-4.5f, 0.5f)), transform.rotation);
            enemyCount++;
        }


        // go to game over screen if hp reaches 0
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Combat");
        }
        // update player health bar
        healthBar.value = playerHealth;

        // check the wave timer
        if (nextWaveTime < Time.time)
        {
            // this wave/level is over, go to combat resolution

            saveData.levelsBeaten = waveNumber + 1;
            Debug.Log(itemsLooted);
            saveData.itemsLootedOverall += itemsLooted;
            saveData.enemiesBeaten = enemiesBeaten;
            saveData.enemiesBeatenOverall += enemiesBeaten;
            saveData.itemsLooted = itemsLooted;
            dataManager.SaveGame();
            SceneManager.LoadScene("CombatResolution");
        }
        waveProgressionBar.value = waveLength + (Time.time - nextWaveTime);
        // if its at the end, go to loot screen

    }
    
    void StartWave()
    {
        waveNumber = saveData.levelsBeaten;

        currentWave = waves[waveNumber];

        waveLength = currentWave.length;
        nextWaveTime = Time.time + waveLength;
    }

    // sets up weapons when the scene starts
    void WeaponInitialization()
    {
        // load buttons in
        foreach (GameObject weaponButton in weaponButtons)
        {
            weaponButton.transform.position = new Vector2(weaponButton.transform.position.x, 30);
            weaponButton.SetActive(false);
        }
        weaponButtons[activeWeaponId].transform.position = new Vector2(weaponButtons[activeWeaponId].transform.position.x, 50);

        // load weapons in
        for (int i = 0; i < saveData.selectedItems.Count; i++)
        {
            equippedWeapons[i] = Instantiate(saveData.selectedItems[i]);
            equippedWeapons[i].transform.SetParent(weaponParent);
            equippedWeapons[i].transform.localPosition = new Vector3(0, 0, 1);
            equippedWeapons[i].SetActive(true);
            numOfEquippedWeapons += 1;
            weaponButtons[i].SetActive(true);
            weaponButtons[i].GetComponent<Image>().sprite = equippedWeapons[i].GetComponent<GunController>().displayImage;
        }

        // set active state
        for (int i = 0; i < numOfEquippedWeapons; i++)
        {
            equippedWeapons[i].SetActive(false);
        }
        equippedWeapons[activeWeaponId].SetActive(true);


        

    }

    // use scroll wheel and number keys to cycle through weapons
    void WeaponManagement()
    {
        // inputs (keys are offset by 1 because 0 is on the other side of the keyboard)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId = 3;
        }

        if (cycleValue > 0)
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId += 1;
            if (activeWeaponId > numOfEquippedWeapons - 1)
            {
                activeWeaponId = 0;
            }
        }
        if (cycleValue < 0)
        {
            pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
            activeWeaponId -= 1;
            if (activeWeaponId < 0)
            {
                activeWeaponId = numOfEquippedWeapons - 1;
            }
        }





        // update weapons
        weaponButtons[pastActiveWeaponId].transform.position = new Vector2(weaponButtons[pastActiveWeaponId].transform.position.x, 30);
        weaponButtons[activeWeaponId].transform.position = new Vector2(weaponButtons[activeWeaponId].transform.position.x, 50);

        if (pastActiveWeaponId != activeWeaponId)
        {
            equippedWeapons[pastActiveWeaponId].SetActive(false);
            Destroy(equippedWeapons[pastActiveWeaponId].GetComponent<GunController>().persistentProjectile);
        }
        equippedWeapons[activeWeaponId].SetActive(true);
    }

    // function that can be called by the weapon buttons that swaps the weapon to the given id
    public void SetWeaponActive(int id)
    {
        pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
        activeWeaponId = id;
    }
}
