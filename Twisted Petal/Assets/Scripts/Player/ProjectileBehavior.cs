using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public int pierce; // decreased by one every time it hits an enemy, once it reaches zero, the projectile is destroyed
    public GameObject targetIndicator;
    private Rigidbody2D rb;

    public enum MunitionType
    {
        None,
        Basic,
        Explosive, // explosive effects & persistent effects
        Laser,
        Arcing, // chain lightning and the like
        Missile // multiple projectiles at once / arcing projectiles
    }

    public MunitionType type;
    
    [Header("Effect Objects")]
    public GameObject effect;

    [Header("Extras")] // I have included notes on which types use what
    public Vector2 targetPosition; // Explosive, Laser
    public Vector2 startingPosition; // Laser, Missile, Arcing
    public bool atTarget = false; // Explosive
    public float targetLength; // Laser
    public bool damagePulse; // Laser

    public Vector2 startingVelocity; // Missile
    public float magnitude; // Missile
    public float frequency; // Missile

    public List<GameObject> pastTargets; // Arcing
    public int positionInTargets; // Arcing
    public int positionModifier = 1; // Arcing
    public bool validTargetsPresent = false; // Arcing

    public float timer;

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        startingPosition = transform.position;
    
        if (type == MunitionType.Missile)
        {
            // convert velocity to degrees
            startingVelocity = rb.linearVelocity;
            magnitude = Random.Range(0, 10);
            frequency = Random.Range(1, 10);
            audioSource.Play();

        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (type == MunitionType.Explosive)
        {
            if (transform.position.x > targetPosition.x)
            {
                GameObject explosion = Instantiate(effect, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(targetIndicator);
                Destroy(gameObject);
            }
        }

        // rapidly teleport back and forth to make the trail effect attached look like a laser beam
        if (type == MunitionType.Laser)
        {
            if (atTarget)
            {
                transform.position = new Vector3(startingPosition.x, startingPosition.y, 2);
                atTarget = false;
            }
            else
            {
                transform.position = new Vector3(targetPosition.x, targetPosition.y, 2);
                atTarget = true;
            }
            // position the laser endpoint
            effect.transform.position = new Vector3(targetPosition.x, targetPosition.y, 1.95f);
        }

        if (type == MunitionType.Arcing)
        {
            if (pierce == 0)
            {
                if (!validTargetsPresent || pastTargets.Count() == 0)
                {
                    Destroy(gameObject);
                }
                else
                {

                    if (positionInTargets < 1)
                    {
                        positionModifier = 1;
                    }
                    if (positionInTargets >= pastTargets.Count - 1)
                    {
                        positionModifier = -1;
                    }
                    positionInTargets += positionModifier;

                    if (pastTargets[positionInTargets] == null)
                    {
                        pastTargets.RemoveAt(positionInTargets);
                    }
                    else
                    {
                        transform.position = pastTargets[positionInTargets].transform.position;
                    }

                    if (timer < Time.time)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (type == MunitionType.Laser)
        {
            LayerMask layerMask = LayerMask.GetMask("Enemies");
            // Does the ray intersect any enemies
            if (Physics2D.Raycast(startingPosition, transform.TransformDirection(Vector3.right), targetLength, layerMask))
            {
                Debug.DrawRay(startingPosition, transform.TransformDirection(Vector3.right) * targetLength, Color.yellow);
                // find the closest colliding enemy
                RaycastHit2D[] results = Physics2D.RaycastAll(startingPosition, transform.TransformDirection(Vector3.right), targetLength, layerMask);

                // take the enemy's position and find how far away it is
                float enemyDistance = Vector2.Distance(startingPosition, results[0].transform.position);
                // then, apply the distance to a normalized vector of the direction the laser object is facing
                Vector2 directionVector = transform.right;
                Vector2 destinationVector = directionVector * enemyDistance;
                // finally, add the starting positon to give us our final vector
                targetPosition = destinationVector + startingPosition;

                // damage enemy
                if (damagePulse)
                {
                    if (results[0].transform.GetComponent<EnemyBehavior>() != null)
                    {
                        results[0].transform.GetComponent<EnemyBehavior>().DamageSelf(damage, EnemyBehavior.DamageType.Fire);
                    }
                    else if (results[0].transform.GetComponent<BossPartDamageTracker>() != null)
                    {
                        results[0].transform.gameObject.GetComponent<BossPartDamageTracker>().DamageSelf(damage);
                    }
                    damagePulse = false;
                }
            }
            else
            {
                Debug.DrawRay(startingPosition, transform.TransformDirection(Vector3.right) * targetLength, Color.white);
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        if (type == MunitionType.Missile)
        {
            // find the perpendicular vector
            Vector2 perpendicularVelocity = new Vector2(-startingVelocity.y, startingVelocity.x);
            // get the mult modifier
            float mult = Mathf.Sin((Time.time + magnitude) * frequency) * 0.2f;
            // use mult to scale the perpdenicular vector appropriately
            perpendicularVelocity = new Vector2(perpendicularVelocity.x * mult, perpendicularVelocity.y * mult);
            // apply velocity
            rb.linearVelocity = (perpendicularVelocity + startingVelocity) / 2.0f;
            
        }

        if (type == MunitionType.Arcing)
        {
            if (pierce > 0)
            {
                GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject targetEnemy = null;
                float distance = 10000;
                for (int i = 0; i < possibleTargets.Length; i++)
                {
                    bool distanceCondition = Vector2.Distance(possibleTargets[i].transform.position, transform.position) < distance;
                    bool iFramesCondition = possibleTargets[i].GetComponent<EnemyBehavior>().invincibilityTimer < Time.time;
                    bool pastTargetsCondition = !pastTargets.Contains(possibleTargets[i]);


                    // if i is closer than the current target and isnt invunlerable and its not already targeted
                    if (distanceCondition && iFramesCondition && pastTargetsCondition)
                    {
                        targetEnemy = possibleTargets[i];
                        distance = Vector2.Distance(possibleTargets[i].transform.position, transform.position);
                    }
                }

                // if there was at least 1 possible target
                if (targetEnemy != null)
                {
                    pastTargets.Add(targetEnemy);
                    transform.position = targetEnemy.transform.position;
                    targetEnemy.GetComponent<EnemyBehavior>().DamageSelf(damage, EnemyBehavior.DamageType.Energy);
                    validTargetsPresent = true;
                }

                pierce--;
                if (pierce == 0)
                {
                    timer = Time.time + 0.5f;
                    pastTargets.Add(targetIndicator);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("EnemyProjectile"))
        {
            if (type == MunitionType.Missile) 
            {
                GameObject explosion = Instantiate(effect, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            } 
            else if (type == MunitionType.Basic)
            {
                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyBehavior>().DamageSelf(damage, EnemyBehavior.DamageType.Bullet);
                }
                else if (other.CompareTag("Boss"))
                {
                    other.GetComponent<BossPartDamageTracker>().DamageSelf(damage);
                }

                if (pierce > 0)
                {
                    pierce -= 1;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}