using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    private GameManagement gameManager;
    public float damage;
    public int pierce; // decreased by one every time it hits a projectile, once it reaches zero, the enemy projectile is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        
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
