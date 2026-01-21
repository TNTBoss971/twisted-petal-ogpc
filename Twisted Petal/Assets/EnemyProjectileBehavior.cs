using UnityEngine;
using UnityEngine.Audio;
using static ProjectileBehavior;

public class EnemyProjectileBehavior : MonoBehaviour
{
    private GameManagement gameManager;
    public float damage;
    public int pierce; // decreased by one every time it hits a projectile, once it reaches zero, the enemy projectile is destroyed
    private Rigidbody2D rb;

    [Header("Missile")]
    public bool isMissile;

    public Vector2 startingVelocity; // Missile
    public float magnitude; // Missile
    public float frequency; // Missile
    public Vector2 startingPosition; // Missile

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        rb = gameObject.GetComponent<Rigidbody2D>();


        startingPosition = transform.position;

        if (isMissile)
        {
            // convert velocity to degrees
            startingVelocity = rb.linearVelocity;
            magnitude = Random.Range(0, 10);
            frequency = Random.Range(1, 10);
            //audioSource.Play();

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMissile)
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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Projectile"))
        {

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gameManager.playerHealth -= damage;
            Destroy(gameObject);
        }
    }

}
