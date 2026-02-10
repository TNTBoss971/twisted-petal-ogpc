using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    private float invincibilityTimer = 0f;
    

    public GameManagement gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (invincibilityTimer <= Time.time && other.GetComponent<EnemyBehavior>().dealsContactDamage)
            {
                gameManager.playerHealth -= 1;
                invincibilityTimer = Time.time + 0.3f;
            }
        }
    }
}
