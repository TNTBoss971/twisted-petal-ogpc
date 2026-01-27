using System;
using UnityEngine;
using static ProjectileBehavior;

public class ExplosionBehavior : MonoBehaviour
{
    public float lifetime;
    public float collisionTime; // how long the collider will remain active
    private float deathTime;
    private float colliderDeathTime;
    public float maxSize;
    public float growthTime; // not utilized yet, the animations will have a growing explosion, so it will look wierd if the collision box also doen't grow
    public float damage;

    public enum AreaType {
        Explosive,
        Poison,
        Stun
    }
    public AreaType type = AreaType.Explosive;
    private AudioSource audioSource;


    // might also make a timer that deletes the collider before deleting the gameobject, this would also us to make the end part of the animation (smoke or something) not damage the enemies

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTime = Time.time + lifetime;
        colliderDeathTime = Time.time + collisionTime;
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTime < Time.time)
        {
            Destroy(gameObject);
        }
        if (colliderDeathTime < Time.time)
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter");
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            if (type == AreaType.Explosive)
            {
                other.GetComponent<EnemyBehavior>().DamageSelf(damage, EnemyBehavior.DamageType.Fire);
            }
            if (type == AreaType.Poison)
            {
                other.GetComponent<EnemyBehavior>().poison += damage;
            }
        }
        if (other.CompareTag("Boss"))
        {
            if (type == AreaType.Explosive)
            {
                other.GetComponent<BossPartDamageTracker>().DamageSelf(damage);
            }
            if (type == AreaType.Poison)
            {
                other.GetComponent<BossPartDamageTracker>().manager.poison += damage;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider Enter");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (type == AreaType.Explosive)
            {
                collision.gameObject.GetComponent<EnemyBehavior>().DamageSelf(damage, EnemyBehavior.DamageType.Fire);
            }
            if (type == AreaType.Poison)
            {
                collision.gameObject.GetComponent<EnemyBehavior>().poison += damage;
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (type == AreaType.Explosive)
            {
                collision.gameObject.GetComponent<BossPartDamageTracker>().DamageSelf(damage);
            }
            if (type == AreaType.Poison)
            {
                collision.gameObject.GetComponent<BossPartDamageTracker>().manager.poison += damage;
            }
        }
        
    }
}
