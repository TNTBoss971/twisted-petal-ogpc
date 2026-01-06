using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public bool testing; // delete when not needed.

    [Header("Spawn In On Load")]
    public GameObject bossPrefab; // prefab of the boss itself
    public Canvas canvasPrefab; // prefab of the special canvas for the boss ui

    [Header("Objects")]
    public GameObject bossObject;
    public Canvas canvasObject;
    public GameObject[] weakpoints;
    public GameObject[] bossParts;
    public GameManagement gameManager;

    [Header("Spawnables")]
    public GameObject[] projectiles;
    public GameObject[] minions;

    // enums
    public enum BossStates // comments in this denote which states are used by which bosses
    {
        None,
        Slam, // Placeholder
        FireProjectile, // Placeholder
        SpawnMinions, // Placeholder (this attack creates minions)
        Exposed // Placeholder (when the player gets to deal the most damage / when the best weakpoints are revealed)
    }
    public enum Bosses
    {
        None,
        Placeholder
    }

    [Header("Control")]
    public Bosses boss; // which boss are we controlling?
    public BossStates bossState; // what is the boss currently doing?
    public BossStates[] attackPattern; // what should the boss do?
    public int positionInAttackPattern; // what was the last thing the boss did?
    public float attackStartTime; // for attacks that last a certain period of time
    private bool damageApplied;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // spawn in the needed objects.
        if (!testing)
        {
            bossObject = Instantiate(bossPrefab);
            canvasObject = Instantiate(canvasPrefab);
        }
        if (boss == Bosses.Placeholder)
        {
            weakpoints = GameObject.FindGameObjectsWithTag("Weakpoint");
            bossParts = GameObject.FindGameObjectsWithTag("Boss");
        }

        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        // if the boss is idle, activate next attack
        if (bossState == BossStates.None)
        {
            // if boss is the placeholder boss
            if (boss == Bosses.Placeholder)
            {
                PlaceholderBehavior();
            }
        } else
        {
            PlaceholderChecks();
        }
    }

    // controls health, the health bar, and what to do upon health reaching 0
    void HealthLogic()
    {
        
    }

    // for unique behavior for different bosses. (dont want all bosses to be the exact same.)
    // wait, are we going to have more than one boss? oh well, its *future proofing*!!!
    void PlaceholderBehavior()
    {
        BossStates currentAttack = attackPattern[positionInAttackPattern];

        // slam attack
        if (currentAttack == BossStates.Slam)
        {
            bossState = BossStates.Slam;
            attackStartTime = Time.time;

            GameObject arm = bossParts[1];
            arm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play slam animation
            bossObject.GetComponent<Animator>().Play("PlaceholderBossSlam");
        }
        
        // projectile attack
        if (currentAttack == BossStates.FireProjectile)
        {
            bossState = BossStates.FireProjectile;
            attackStartTime = Time.time;

            GameObject arm = bossParts[1];
            arm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play firing animation
            Debug.Log("Firing Projectiles");
            bossObject.GetComponent<Animator>().Play("PlaceholderBossFireProjectile");
            //FinishAttack();
        }

        // minion attack
        if (currentAttack == BossStates.SpawnMinions)
        {
            bossState = BossStates.SpawnMinions;
            attackStartTime = Time.time;

            GameObject arm = bossParts[1];
            arm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play spawing animation
            Debug.Log("Spawing Minions");
            bossObject.GetComponent<Animator>().Play("PlaceholderBossSpawnMinions");
            //FinishAttack();
        }

        // exposed
        if (currentAttack == BossStates.Exposed)
        {
            bossState = BossStates.Exposed;
            attackStartTime = Time.time;

            GameObject arm = bossParts[1];
            arm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play exposed animation
            Debug.Log("Exposed");
            bossObject.GetComponent<Animator>().Play("PlaceholderBossExposed");
            //FinishAttack();
        }
    }
    // determines if the current attack should be over
    void PlaceholderChecks()
    {
        // slam
        if (bossState == BossStates.Slam)
        {
            GameObject arm = bossParts[1];
            if (arm.GetComponent<BossPartDamageTracker>().damageThisAttack >= 20)
            {
                // cancel the attack

            } 
            else if (attackStartTime + 2.05f <= Time.time)
            {
                FinishAttack();
            }
            else if (attackStartTime + 1.45f <= Time.time && !damageApplied)
            {
                // deal the damage of the attack
                gameManager.playerHealth -= 10;
                damageApplied = true;
            }
        }

        // exposed
        if (bossState == BossStates.Exposed)
        {
            if (attackStartTime + 2.30f <= Time.time)
            {

                FinishAttack();
            }
        }

        // spawn minions
        if (bossState == BossStates.SpawnMinions)
        {
            if (attackStartTime + 1.40f <= Time.time)
            {

                FinishAttack();
            }
            else if (attackStartTime + 1.00f <= Time.time && !damageApplied)
            {
                for (int i = 0; i < 10; i++)
                {
                    GameObject newMinion = Instantiate(minions[0]);
                    newMinion.transform.position = new Vector3(-1 + 0.25f * i, -3, 0);
                }
                damageApplied = true;
            }
        }

        // fire projectile
        if (bossState == BossStates.FireProjectile)
        {
            if (attackStartTime + 1.35f <= Time.time)
            {

                FinishAttack();
            }
        }
    }

    public void FinishAttack()
    {
        // reset the boss state
        bossState = BossStates.None;

        // increment the position in the attack pattern
        positionInAttackPattern++;
        if (positionInAttackPattern >= attackPattern.Length)
        {
            positionInAttackPattern = 0;
        }
    }
}
