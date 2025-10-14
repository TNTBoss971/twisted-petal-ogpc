using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}