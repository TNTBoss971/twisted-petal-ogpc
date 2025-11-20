using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public int pierce; // decreased by one every time it hits an enemy, once it reaches zero, the projectile is destroyed

    public enum MunitionType
    {
        None,
        Basic,
        Explosive, // explosive effects
        Laser,
        Gas, // persistent effects
        Arcing // chain lightning and the lke
    }
    public MunitionType type;

    [Header("Explosive")]
    public Vector2 targetPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (type == MunitionType.Explosive)
        {
            if (transform.position.x > targetPosition.x)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
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