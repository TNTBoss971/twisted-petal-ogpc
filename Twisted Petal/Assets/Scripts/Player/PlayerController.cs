using UnityEngine;
using UnityEngine.InputSystem;
using static EnemyBehavior;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    public float invincibilityTimer = 0f;
    
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
        /*
        if (transform.position.y > downBoundary && Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - vanSpeed * Time.deltaTime);
        }
        if (transform.position.y < upBoundary && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + vanSpeed * Time.deltaTime);
        }*/
    }

    
    public void DamageSelf(float damage)
    {
        if (invincibilityTimer <= Time.time)
        {
            gameManager.playerHealth -= damage;
            invincibilityTimer = Time.time + 0.3f;
        }
    }
}
