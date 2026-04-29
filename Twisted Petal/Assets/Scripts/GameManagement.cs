using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public bool devSkipActive;

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
    public int itemsLooted;
    public int enemiesBeaten;

    public WaveData[] waves; // a list of all the waves
    public WaveData currentWave;
    public BossManager bossManager;
    private float spawnCooldown;
    [Header("Status Bars")]
    public BarBehavior waveProgressionBar;
    public GameObject uiRoad;
    public BarBehavior healthBar;
    public GameObject playerHealthText;
    public GameObject shootHint;
    [Header("Save Data")]
    public DataPersistanceManager dataManager;
    public DataManagement saveData;
    [Header("Summary")]
    public LevelSummaryCreator summaryCreator;
    public GameObject lastWeaponObtained;
    [Header("Pausing/Loading In")]
    public bool paused;
    public GameObject pauseHue;
    private GameObject fadeBox;
    private string sceneToLoad;
    private bool statsSaved;
    public ScrollGround[] backgroundControllers;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeBox = GameObject.Find("FadeBox");
        fadeBox.GetComponent<Animator>().Play("FadeIn");
        dataManager.LoadGame();
        
        itemsLooted = 0;
        enemiesBeaten = 0;
        // assign actions
        cycleAction = InputSystem.actions.FindAction("Cycle");

        playerMaxHealth = saveData.maxHealth;
        playerHealth = saveData.currentHealth;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        if (saveData.levelsBeaten > 0)
        {
            shootHint.GetComponent<TMPro.TextMeshProUGUI>().text = "W and S to move up and down.";
        }
        else
        {
            shootHint.GetComponent<TMPro.TextMeshProUGUI>().text = "Left Click to shoot. \n W and S to move up and down.";
        }

        statsSaved = false;

        StartWave();

        waveProgressionBar.maxValue = waveLength;

        healthBar.maxValue = playerMaxHealth;

        lastWeaponObtained = null;

        paused = false;

        pauseHue.SetActive(false);
        
        WeaponInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        cycleValue = cycleAction.ReadValue<float>(); // read the value of the scroll wheel

        WeaponManagement();


        ActiveWave();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                paused = true;
                pauseHue.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                paused = false;
                pauseHue.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
    void ActiveWave()
    {
        if (currentWave.isBossBattle)
        {
            Debug.Log(bossManager.health);
            if (bossManager.health <= 0)
            {
                EndWave();
            }
        }
        else
        {
            // continuouslly spawn enemies while wave is active
            // rarely spawn "loot" enemy
            if (enemyCount < enemyCountMax && spawnCooldown <= Time.time)
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
                    }
                    else
                    {
                        enemyIndex++;
                    }
                }

                GameObject clone = Instantiate(currentWave.enemiesInWave[enemyIndex], new Vector2(11 - Random.Range(-2.5f, 0.5f), Random.Range(-4.5f, 0.5f)), transform.rotation);
                enemyCount++;
                spawnCooldown = Time.time + currentWave.spawnrate;
            }

            // check the wave timer
            if (nextWaveTime < Time.time || (devSkipActive && Input.GetKeyDown(KeyCode.Space)))
            {
                EndWave();
            }
            waveProgressionBar.value = waveLength + (Time.time - nextWaveTime);

        }


        // go to game over screen if hp reaches 0
        if (playerHealth <= 0)
        {
            KillPlayer();
        }
        // update player health bar
        healthBar.value = playerHealth;

    }
    
    void StartWave()
    {
        waveNumber = saveData.levelsBeaten;

        currentWave = waves[waveNumber];

        enemyCountMax = currentWave.maxEnemies;

        if (currentWave.isBossBattle)
        {
            bossManager = Instantiate(currentWave.enemiesInWave[0]).GetComponent<BossManager>();
            uiRoad.SetActive(false);
            waveProgressionBar.gameObject.SetActive(false);
        }
        else
        {
            waveLength = currentWave.length;
            nextWaveTime = Time.time + waveLength;    
        }

        // set up backgrounds
        ScrollGround frontBackgrounds = backgroundControllers[0];
        frontBackgrounds.speed = currentWave.frontBackgroundSpeed;
        frontBackgrounds.road0.GetComponent<SpriteRenderer>().sprite = currentWave.frontBackground;
        frontBackgrounds.road1.GetComponent<SpriteRenderer>().sprite = currentWave.frontBackground;
        ScrollGround backBackgrounds = backgroundControllers[1];
        backBackgrounds.speed = currentWave.backBackgroundSpeed;
        backBackgrounds.road0.GetComponent<SpriteRenderer>().sprite = currentWave.backBackground;
        backBackgrounds.road1.GetComponent<SpriteRenderer>().sprite = currentWave.backBackground;
        
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
            SetWeaponActive(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeaponActive(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeaponActive(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeaponActive(3);
        }

        if (cycleValue > 0)
        {
            int tempId = activeWeaponId + 1;
            if (tempId > numOfEquippedWeapons - 1)
            {
                tempId = 0;
            }
            SetWeaponActive(tempId);
        }
        if (cycleValue < 0)
        {
            int tempId = activeWeaponId - 1;
            if (tempId < 0)
            {
                tempId = numOfEquippedWeapons - 1;
            }
            SetWeaponActive(tempId);
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
    public void EndWave()
    {
        Debug.Log("Comfirm" + Time.time);
        // this wave/level is over, go to combat resolution
        if (statsSaved == false)
        {
            saveData.levelsBeaten = waveNumber + 1;
            saveData.itemsLootedOverall += itemsLooted;
            saveData.enemiesBeaten = enemiesBeaten;
            saveData.enemiesBeatenOverall += enemiesBeaten;
            saveData.itemsLooted = itemsLooted;
            saveData.levelSummaries.Add(summaryCreator.CreateSummary(saveData, playerHealth, lastWeaponObtained));
            saveData.currentHealth = playerHealth;
            dataManager.SaveGame();
            statsSaved = true;
        }
        sceneToLoad = "CombatResolution";
        fadeBox.GetComponent<Animator>().Play("FadeOut");
        Invoke(nameof(LoadSceneForDelay), 1f);
        Debug.Log("Comfirm");
    }
    public void KillPlayer()
    {
        sceneToLoad = "WorldMap";
        fadeBox.GetComponent<Animator>().Play("FadeOut");
        Invoke(nameof(LoadSceneForDelay), 1f);
    }
    public void LoadSceneForDelay()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // function that can be called by the weapon buttons that swaps the weapon to the given id
    public void SetWeaponActive(int id)
    {
        pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
        activeWeaponId = id;
        equippedWeapons[activeWeaponId].SetActive(true);
        equippedWeapons[activeWeaponId].GetComponent<GunController>().WakeUp();
    }
}
