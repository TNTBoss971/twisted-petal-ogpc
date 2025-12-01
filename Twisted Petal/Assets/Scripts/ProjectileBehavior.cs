using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public int pierce; // decreased by one every time it hits an enemy, once it reaches zero, the projectile is destroyed
    public GameObject targetIndicator;

    

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
    
    [Header("Effect Objects")]
    public GameObject effect;

    [Header("Extras")]
    public Vector2 targetPosition;
    public Vector2 startingPosition;
    public bool atTarget = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (type == MunitionType.Explosive)
        {
            if (transform.position.x > targetPosition.x)
            {
                GameObject explosion = Instantiate(effect, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(targetIndicator);
                Destroy(gameObject);
            }
        }
        if (type == MunitionType.Laser)
        {
            if (atTarget)
            {
                transform.position = startingPosition;
                atTarget = false;
            } 
            else
            {
                transform.position = targetPosition;
                atTarget = true;
            }

        }
    }
    void FixedUpdate()
    {
        if (type == MunitionType.Laser)
        {
            LayerMask layerMask = LayerMask.GetMask("Enemies");
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics2D.Raycast(startingPosition, transform.TransformDirection(Vector3.right) * 100, 100, layerMask))
            {
                Debug.DrawRay(startingPosition, transform.TransformDirection(Vector3.right) * 100, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(startingPosition, transform.TransformDirection(Vector3.right) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") && type == MunitionType.Basic)
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