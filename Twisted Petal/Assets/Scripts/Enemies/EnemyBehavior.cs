using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    public enum DamageType
    {
        None,
        Unknown,
        Bullet, // for most interactions, includes i-frames
        Tick, // tick damage ignores i-frames, preventing poison from making an enemy invincible
        Fire, // burn the world! >:D
        Energy // slower i-frames
    }
    public enum EnemyType
    {
        Basic, //bush guy, can only attack by hitting the van, lot of them
        Ranged, //long range needle attack
        Stump,
        Flung
    }

    public EnemyType type;

    [Header("Targeting")]
    public Transform target;
    private GameObject player;
    private PlayerController playerController;
    private GameManagement gameManager;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool hasLoot = false;
    public int lootFrequency; // the higher this number is the less likely it is for a loot drop
    private ParticleSystem lootSparkles;

    [Header("Attributes")]
    public float speed = 3f;
    public float maxHealth = 2f;
    public float health = 2f;
    public float damage = 1f;
    public float poisonPerTick = 1f; // how much damage the enemy takes from poison each tick
    public bool dealsContactDamage;

    [Header("Status")]
    public float poison = 0;
    public bool hasNotTickedDamage = true;
    public float invincibilityTimer = 0f;
    public bool dealDamage = false;

    [Header("Display")]
    public Animator animator;
    public bool hasIntro = false;
    public float introLength;

    public string attackAnimationName;
    public float attackAnimationCycleLength;
    public string walkAnimationName;

    [Header("Logic")]
    private float leftBoundary;
    public bool isMoving = true;
    public bool isMinion;
    private float spawnTime;

    [Header("Item Looting")]
    private DataManagement saveData;
    private int rarityChance;
    private ItemPopup itemPopup;

    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        target = player.transform;
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        if (Random.Range(0, lootFrequency) == (lootFrequency - 1) && !isMinion)
        {
            hasLoot = true;
        }
        lootSparkles = this.GetComponent<ParticleSystem>();

        health = maxHealth;

        saveData = gameManager.GetComponent<DataManagement>();
        itemPopup = FindObjectsByType<ItemPopup>(FindObjectsSortMode.None)[0];

        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {   
        if (hasLoot == false)
        {
            if (lootSparkles != null)
            {
                Destroy(lootSparkles);
            }
        }

        if (health < 1)
        {
            if (hasLoot == true)
            {
                gameManager.itemsLooted += 1;
                //adds looted items to the player's inventory
                WaveData wave = gameManager.currentWave;
                GameObject[] weaponPool = wave.weaponsInWave;
                float selectedFreq = Random.Range(0.001f, 1);
                float[] frequencies = wave.weaponFrequency;
                int weaponIndex = 0;

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
                        weaponIndex++;
                    }
                }

                GameObject itemLooted = weaponPool[weaponIndex];
                gameManager.saveData.ownedItems.Add(itemLooted);
                itemPopup.displayPopup("You got a " + itemLooted.GetComponent<GunController>().weaponName + "!");
                gameManager.lastWeaponObtained = itemLooted;
            }
            gameManager.enemiesBeaten += 1;
            gameManager.enemyCount -= 1;
            Destroy(gameObject);
        }

        if (type == EnemyType.Stump)
        {
            Vector3 direction = new Vector2(-1, 0);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
            if (isMoving == true)
            {
                rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
            }
        }
        

        if (dealDamage && Time.time % attackAnimationCycleLength <= 0.1)
        {
            player.GetComponent<PlayerController>().DamageSelf(damage);
        }
    }

    private void FixedUpdate()
    {
        if (!hasIntro)
        {
            MoveCharacter(movement);
            BehaviorLogic();

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
        else
        {
            if (spawnTime + introLength < Time.time)
            {
                hasIntro = false;
            }
        }
    }

    void BehaviorLogic()
    {
        if (type == EnemyType.Ranged) {
            if (transform.position.x < leftBoundary)
            {
                isMoving = false;
            }
        }
    }

    void MoveCharacter(Vector2 direction)
    {
    }
        

    // for all your tick damage related needs
    private void DamageTick()
    {
        // check for and apply poison
        if (poison > 0)
        {
            // poison cap
            if (poison >= maxHealth * 10)
            {
                DamageSelf(poison, DamageType.Tick);
            }

            if (poison >= poisonPerTick)
            {
                poison -= poisonPerTick;
                DamageSelf(poisonPerTick, DamageType.Tick);
            }
            else
            {
                DamageSelf(poison, DamageType.Tick);
                poison = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dealsContactDamage)
            {
                isMoving = false;
                animator.Play(attackAnimationName);
                dealDamage = true;
            }
            else
            {
                if (type == EnemyType.Stump)
                {
                    gameManager.playerHealth -= damage;
                    Destroy(gameObject);
                }
                else if (playerController.invincibilityTimer <= Time.time && dealsContactDamage)
                {
                    gameManager.playerHealth -= damage;
                    playerController.invincibilityTimer = Time.time + 0.3f;
                }
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // if the enemy doesnt deal contact damage and is far enough away
            if (!dealsContactDamage && Mathf.Abs(gameObject.transform.position.x - collision.gameObject.transform.position.x) > 2.5f)
            {
                isMoving = true;
                animator.Play(walkAnimationName);
                dealDamage = false;
            }
        }
    }


    // called by projectiles and the like
    // the reason why we're doing it this way is becuase it offers more control on how enemies are damaged
    public void DamageSelf(float damage, DamageType type = DamageType.Unknown)
    {
        if (type == DamageType.Unknown)
        {
            Debug.Log("Something has gone wrong and damage type wasn't assigned");
            return;
        }

        if (invincibilityTimer <= Time.time || type == DamageType.Tick)
        {
            health -= damage;
            if (type != DamageType.Tick) // tick damage doesnt give i-frames
            {
                if (type == DamageType.Energy)
                {
                    invincibilityTimer = Time.time + 0.2f;
                } 
                else
                {
                    invincibilityTimer = Time.time + 0.01f;
                }
            }
        }
    }
}