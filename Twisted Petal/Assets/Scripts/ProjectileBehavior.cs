using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 0, 3);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 20);
        if (transform.position.x > player.transform.position.x + 100 || transform.position.y > player.transform.position.y + 100 || transform.position.x < player.transform.position.x - 100 || transform.position.y < player.transform.position.y - 100)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}