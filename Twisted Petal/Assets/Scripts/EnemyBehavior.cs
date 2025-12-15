using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum DamageType
    {
        None,
        Unknown,
        Bullet, // for most interactions, includes i-frames
        Tick, // tick damage ignores i-frames, preventing poison from making an enemy invincible
        Fire, // burn the world! >:D
        Energy // slower i-frames
    }

    [Header("Targeting")]
    public Transform target;
    private GameObject player;
    private GameManagement gameManager;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float health;
    private float colorTime = 0f;
    private string color = "default";
    private float invincibilityTimer = 0f;
    private float speed = 3f;
    private GameManagement gameManager;
    private bool hasLoot = false;
    public int lootFrequency; // the higher this number is the less likely it is for a loot drop
    private ParticleSystem lootSparkles;

    [Header("Attributes")]
    public float speed = 3f;
    public float maxHealth = 2f;
    public float health = 2f;
    public float poisonPerTick = 1f; // how much damage the enemy takes from poison each tick
    [Header("Status")]
    public float poison = 0;
    public bool hasNotTickedDamage = true;
    public float invincibilityTimer = 0f;

    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        player = GameObject.Find("Player");
        target = player.transform;
        rb = this.GetComponent<Rigidbody2D>();
        health = 2f;
        if (Random.Range(0, lootFrequency) == (lootFrequency - 1))
        {
            hasLoot = true;
        }
        lootSparkles = this.GetComponent<ParticleSystem>();
        health = maxHealth;
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
                    GameManagement.itemsLooted += 1;
                }
                GameManagement.enemiesBeaten += 1;
                gameManager.enemyCount -= 1;
                Destroy(gameObject);
            }
        /*
        if (color == "default")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        */
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;



        
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
        
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

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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