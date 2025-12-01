using UnityEngine;
using static ProjectileBehavior;

public class ExplosionBehavior : MonoBehaviour
{
    public float lifetime;
    private float deathTime;
    public float maxSize;
    public float growthTime; // not utilized yet, the animations will have a growing explosion, so it will look wierd if the collision box also doen't grow
    public float damage;

    // might also make a timer that deletes the collider before deleting the gameobject, this would also us to make the end part of the animation (smoke or something) not damage the enemies

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTime = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTime < Time.time)
        {
            Destroy(gameObject);
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
        }

    }
}
