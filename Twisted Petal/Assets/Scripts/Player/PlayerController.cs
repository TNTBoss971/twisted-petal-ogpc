using UnityEngine;
using UnityEngine.InputSystem;
using static EnemyBehavior;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    private float invincibilityTimer = 0f;
    
    public float vanSpeed;
    public float upBoundary;
    public float downBoundary;
    public GameManagement gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > downBoundary && Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - vanSpeed * Time.deltaTime);
        }
        if (transform.position.y < upBoundary && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + vanSpeed * Time.deltaTime);
        }
    }

    void EnemyEnter(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyBehavior>().type == EnemyType.Stump)
        {
            gameManager.playerHealth -= enemy.GetComponent<EnemyBehavior>().damage;
            Destroy(enemy);
        }
        else if (invincibilityTimer <= Time.time && enemy.GetComponent<EnemyBehavior>().dealsContactDamage)
        {
            gameManager.playerHealth -= enemy.GetComponent<EnemyBehavior>().damage;
            invincibilityTimer = Time.time + 0.3f;
        }
    }
    
    public void DamageSelf(float damage)
    {
        if (invincibilityTimer <= Time.time)
        {
            gameManager.playerHealth -= damage;
            invincibilityTimer = Time.time + 0.3f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyEnter(other.gameObject);
            
        } /* else if (other.CompareTag("EnemyProjectile"))
        {
            
            if (invincibilityTimer <= Time.time)
            {
                gameManager.playerHealth -= other.GetComponent<EnemyProjectileBehavior>().damage;
                invincibilityTimer = Time.time + 0.3f;
            }
        } */
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyEnter(other.gameObject);
        }
        else if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            
            if (invincibilityTimer <= Time.time)
            {
                gameManager.playerHealth -= other.gameObject.GetComponent<EnemyProjectileBehavior>().damage;
                invincibilityTimer = Time.time + 0.3f;
            }
        }
    }
}
