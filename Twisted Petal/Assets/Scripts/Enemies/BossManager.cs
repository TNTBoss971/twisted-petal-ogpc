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
    private GameObject mainBody;
    private GameObject frontArm;
    private GameObject backArm;

    [Header("Spawnables")]
    public GameObject[] projectiles;
    public GameObject[] minions;

    // enums
    public enum BossStates // comments in this denote which states are used by which bosses
    {
        None,
        Slam, // Plant
        FireProjectile, // Plant
        SpawnMinions, // Plant (this attack creates minions)
        Exposed, // Plant (when the player gets to deal the most damage / when the best weakpoints are revealed)
        Stunned, // Plant (when the player stops an attack)
        Intro // for super cool epic intros!!!!
    }
    public enum Bosses
    {
        None,
        PlantBoss,
        Placeholder
    }

    [Header("Control")]
    public Bosses boss; // which boss are we controlling?
    public BossStates bossState; // what is the boss currently doing?
    public BossStates[] attackPattern; // what should the boss do?
    public int positionInAttackPattern; // what was the last thing the boss did?
    public float attackStartTime; // for attacks that last a certain period of time
    private bool damageApplied;
    private float fireTime;
    private float startingX;

    [Header("Properties")]
    public float maxHealth;
    public float health;
    public float poison;
    public bool hasNotTickedDamage = true;
    public float poisonPerTick = 1f; // how much damage the boss takes from poison each tick

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // spawn in the needed objects.
        if (!testing)
        {
            bossObject = Instantiate(bossPrefab);
            canvasObject = Instantiate(canvasPrefab);
        }
        else
        {
            bossObject = GameObject.Find("PlantBoss");
            canvasObject = GameObject.Find("PlantBossCanvas").GetComponent<Canvas>();
        }
        if (boss == Bosses.PlantBoss)
        {
            weakpoints = GameObject.FindGameObjectsWithTag("Weakpoint");
            bossParts = GameObject.FindGameObjectsWithTag("Boss");
            mainBody = bossParts[0];
            frontArm = bossParts[1];
            backArm = bossParts[2];
            bossState = BossStates.Intro;
            attackStartTime = Time.time;
        }

        startingX = bossObject.transform.position.x;

        BarBehavior bossHealthBar = canvasObject.GetComponentInChildren<BarBehavior>();
        bossHealthBar.maxValue = maxHealth;

        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];

    }

    // Update is called once per frame
    void Update()
    {
        // if the boss is idle, activate next attack
        if (bossState == BossStates.None)
        {
            // if boss is the plant boss
            if (boss == Bosses.PlantBoss)
            {
                PlantBossBehavior();
            }
        } 
        else
        {
            PlantBossChecks();
        }
        HealthLogic();
    }
    void FixedUpdate()
    {
        if (bossState == BossStates.Stunned)
        {
            Stunned();
        }
        // in my testing, Time.time % 10f will never be exactly zero
        if (Time.time % 10f <= 10f && hasNotTickedDamage)
        {
            hasNotTickedDamage = false;
            DamageTick();
        }

        if (Time.time % 0.1f >= 0.09f)
        {
            hasNotTickedDamage = true;
        }
    }
    // for all your tick damage related needs
    private void DamageTick()
    {
        // check for and apply poison
        if (poison > 0)
        {
            // bosses do not have a poison cap

            if (poison >= poisonPerTick)
            {
                poison -= poisonPerTick;
                health -= poisonPerTick;
            }
            else
            {
                health -= poison;
                poison = 0;
            }
        }
    }

    // controls health, the health bar, and what to do upon health reaching 0
    void HealthLogic()
    {
        BarBehavior bossHealthBar = canvasObject.GetComponentInChildren<BarBehavior>();
        bossHealthBar.value = health;
    }

    // for unique behavior for different bosses. (dont want all bosses to be the exact same.)
    // wait, are we going to have more than one boss? oh well, its *future proofing*!!!
    // screw future proofing >:(
    void PlantBossBehavior()
    {
        BossStates currentAttack = attackPattern[positionInAttackPattern];

        // slam attack
        if (currentAttack == BossStates.Slam)
        {
            bossState = BossStates.Slam;
            attackStartTime = Time.time;

            frontArm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play slam animation
            bossObject.GetComponent<Animator>().Play("PlantBossSlam");
        }
        
        // projectile attack
        if (currentAttack == BossStates.FireProjectile)
        {
            bossState = BossStates.FireProjectile;
            attackStartTime = Time.time;

            frontArm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            backArm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            mainBody.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            fireTime = 0;
            // play firing animation
            Debug.Log("Firing Projectiles");
            bossObject.GetComponent<Animator>().Play("PlantBossFireProjectile");
        }

        // minion attack
        if (currentAttack == BossStates.SpawnMinions)
        {
            bossState = BossStates.SpawnMinions;
            attackStartTime = Time.time;

            backArm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play spawning animation
            bossObject.GetComponent<Animator>().Play("PlantBossSpawnMinions");
        }

        // exposed
        if (currentAttack == BossStates.Exposed)
        {
            bossState = BossStates.Exposed;
            attackStartTime = Time.time;

            frontArm.GetComponent<BossPartDamageTracker>().damageThisAttack = 0;
            damageApplied = false;
            // play exposed animation
            bossObject.GetComponent<Animator>().Play("PlantBossExposed");
        }
    }
    // determines if the current attack should be over
    void PlantBossChecks()
    {
        // intro
        if (bossState == BossStates.Intro)
        {
            if (attackStartTime + 1.03f <= Time.time)
            {
                bossState = BossStates.None;
            }
        }

        // slam
        if (bossState == BossStates.Slam)
        {
            if (frontArm.GetComponent<BossPartDamageTracker>().damageThisAttack >= 2)
            {
                FinishAttack(true);
            }
            else if (attackStartTime + 2.45f <= Time.time)
            {
                FinishAttack(false);
            }
            else if (attackStartTime + 2.15f <= Time.time && !damageApplied)
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

                FinishAttack(false);
            }
        }

        // spawn minions
        if (bossState == BossStates.SpawnMinions)
        {
            if (backArm.GetComponent<BossPartDamageTracker>().damageThisAttack >= 20)
            {
                // cancel the attack
                FinishAttack(true);
            }
            if (attackStartTime + 1.40f <= Time.time)
            {

                FinishAttack(false);
            }
            else if (attackStartTime + 1.00f <= Time.time && !damageApplied)
            {
                for (int i = 0; i < Random.Range(1, 3); i++)
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
            if (mainBody.GetComponent<BossPartDamageTracker>().damageThisAttack + 
            frontArm.GetComponent<BossPartDamageTracker>().damageThisAttack + 
            backArm.GetComponent<BossPartDamageTracker>().damageThisAttack >= 20)
            {
                // cancel the attack
                FinishAttack(true);
            }
            if (attackStartTime + 2.08f <= Time.time)
            {
                FinishAttack(false);
            } 
            else if (attackStartTime + 1.00f + fireTime <= Time.time && attackStartTime + 1.44f >= Time.time)
            {
                fireTime += 0.12f;
                GameObject projectile = Instantiate(projectiles[0]);
                projectile.transform.position = new Vector3(3.5f, -0.75f, 0);
                projectile.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * 30;
            }
            else if (!damageApplied && attackStartTime + 1.44 <= Time.time)
            {
                GameObject projectile = Instantiate(projectiles[1]);
                projectile.transform.position = new Vector3(3.5f, -0.75f, 0);
                projectile.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * 10;

                damageApplied = true;
            }
        }
    }

    public void Stunned()
    {
        // bossObject.transform.position = new Vector2 (startingX + ((Time.time - attackStartTime) % 0.3f - 0.15f) / 10, bossObject.transform.position.y);
        bossObject.transform.position = new Vector2(startingX + (float)Mathf.Sin(Time.time * 50) / 40, bossObject.transform.position.y);
        if (attackStartTime + 2 <= Time.time)
        {
            bossObject.transform.position = new Vector2(startingX, bossObject.transform.position.y);
            bossState = BossStates.None;
            
            // unfreeze the animation
            bossObject.GetComponent<Animator>().enabled = true;
            // play idle animation
            bossObject.GetComponent<Animator>().Play("PlantBossIdle");
            FinishAttack(false);
        }
    }
    public void FinishAttack(bool stun)
    {
        if (stun) {
            attackStartTime = Time.time;
            bossState = BossStates.Stunned;
            // freeze the animation
            bossObject.GetComponent<Animator>().enabled = false;
        } else {

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
}
