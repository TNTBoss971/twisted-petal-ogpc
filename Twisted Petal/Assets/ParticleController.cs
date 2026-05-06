using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float particleLength = 1;
    private float deathTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTime = Time.time + particleLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deathTime)
        {
            Destroy(gameObject);
        }
    }
}
