using UnityEngine;
using UnityEngine.InputSystem;

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
    public int activeWeaponId;  // active weapon
    public int pastActiveWeaponId = 1; // for turning off previously active weapons

    [Header("Wave Logic")]
    public int enemyNumber;
    public int waveNumber = 0;
    public GameObject enemy;

    public float waveLength;
    public float nextWaveTime;

    [Header("Status Bars")]
    public BarBehavior waveProgressionBar;
    public BarBehavior healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assign actions
        cycleAction = InputSystem.actions.FindAction("Cycle");




        nextWaveTime = waveLength;
        waveProgressionBar.maxValue = waveLength;

        healthBar.maxValue = playerMaxHealth;

        WeaponInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        cycleValue = cycleAction.ReadValue<float>();

        WeaponManagement();

        if (enemyNumber == 0)
        {
            waveNumber += 1;
            for (int i = 0; i < 4; i++)
            {
                GameObject clone = Instantiate(enemy, new Vector2(5, -1), transform.rotation);
                enemyNumber++;
            }
        }

        // wave timer
        if (nextWaveTime < Time.time)
        {
            nextWaveTime = Time.time + waveLength;
        }
        waveProgressionBar.value = waveLength + (Time.time - nextWaveTime);

        // update player health bar
        healthBar.value = playerHealth;
    }
    void ActiveWave()
    {
        // continuouslly spawn enemies while wave is active
            // rarely spawn "loot" enemy

        // go to game over screen if hp reaches 0

        // check the wave timer
            // if its at the end, go to loot screen

    }

    // sets up weapons when the scene starts
    void WeaponInitialization()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[activeWeaponId].SetActive(true);


        foreach (GameObject weaponButton in weaponButtons)
        {
            weaponButton.transform.position = new Vector2(weaponButton.transform.position.x, 30);
        }

        weaponButtons[activeWeaponId].transform.position = new Vector2(weaponButtons[activeWeaponId].transform.position.x, 50);
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
            if (activeWeaponId > 3)
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
                activeWeaponId = 3;
            }
        }





        // update weapons
        weaponButtons[pastActiveWeaponId].transform.position = new Vector2(weaponButtons[pastActiveWeaponId].transform.position.x, 30);
        weaponButtons[activeWeaponId].transform.position = new Vector2(weaponButtons[activeWeaponId].transform.position.x, 50);

        weapons[pastActiveWeaponId].SetActive(false);
        weapons[activeWeaponId].SetActive(true);
    }

    // function that can be called by the weapon buttons that swaps the weapon to the given id
    public void SetWeaponActive(int id)
    {
        pastActiveWeaponId = activeWeaponId; // deactivate old active weapon
        activeWeaponId = id;
    }
}
