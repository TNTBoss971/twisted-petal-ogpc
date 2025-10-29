using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInput;
    private float speed = 7.0f;
    public GameObject projectile;
    private float gunCooldown = 0f;
    private float invincibilityTimer = 0f;
    public static int playerHealth = 5;
    private float colorTime = 0f;
    private string color = "default";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth < 1)
        {
            Destroy(gameObject);
        }

        if (color == "default")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (color == "red")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (colorTime <= Time.time)
            {
                color = "default";
            }
        }
    
        movementInput = Input.GetAxis("Horizontal");

        this.GetComponent<Rigidbody2D>().rotation = 0;
        
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKey("space"))
        {
            if (gunCooldown <= Time.time)
            {
                GameObject clone;
                clone = Instantiate(projectile, transform.position, transform.rotation);
                gunCooldown = Time.time + 0.3f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (invincibilityTimer <= Time.time)
            {
                playerHealth -= 1;
                color = "red";
                colorTime = Time.time + 0.3f;
                invincibilityTimer = Time.time + 0.3f;
            }
        }
    }
}
