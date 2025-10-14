using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float health;
    private float colorTime = 0f;
    private string color = "default";
    private float invincibilityTimer = 0f;
    private float speed = 3f;
    private GameManagement gameManager;

    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        player = GameObject.Find("Player");
        target = player.transform;
        rb = this.GetComponent<Rigidbody2D>();
        health = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            gameManager.enemyNumber -= 1;
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
    
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (invincibilityTimer <= Time.time)
            {
                health -= 1;
                color = "red";
                colorTime = Time.time + 0.3f;
                invincibilityTimer = Time.time + 0.01f;
            }
            Destroy(other);
        }
    }
}