using UnityEngine;

public class CourseCorrection : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // insures that the projectile is facing in the direction of movement
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90f));
    }
}
