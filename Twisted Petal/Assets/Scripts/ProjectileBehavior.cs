using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

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
        Explosive, // explosive effects
        Laser,
        Gas, // persistent effects
        Arcing, // chain lightning and the like
        Missile // multiple projectiles at once / arcing projectiles
    }
    public MunitionType type;
    
    [Header("Effect Objects")]
    public GameObject effect;

    [Header("Extras")] // I have included notes on which types use what
    public Vector2 targetPosition; // Explosive, Laser
    public Vector2 startingPosition; // Laser, Missile
    public bool atTarget = false; // Explosive
    public float targetLength; // Laser
    public bool damagePulse; // Laser

    public Vector2 startingVelocity; // Missile
    public float offset; // Missile
    public float frequency; // Missile


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");
        startingPosition = transform.position;

        if (type == MunitionType.Missile)
        {
            // convert velocity to degrees
            startingVelocity = rb.linearVelocity;
            offset = Random.Range(0, 10);
            frequency = Random.Range(1, 10);
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
                    results[0].transform.GetComponent<EnemyBehavior>().DamageSelf(damage);
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
            float mult = Mathf.Sin((Time.time + offset) * frequency) * 0.2f;
            // use mult to scale the perpdenicular vector appropriately
            perpendicularVelocity = new Vector2(perpendicularVelocity.x * mult, perpendicularVelocity.y * mult);
            // apply velocity
            rb.linearVelocity = (perpendicularVelocity + startingVelocity) / 2.0f;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") && type == MunitionType.Basic)
        {
            other.GetComponent<EnemyBehavior>().DamageSelf(damage);

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