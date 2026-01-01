using UnityEngine;

public class BossPartDamageTracker : MonoBehaviour
{
    private float totalDamage;
    public float damageThisAttack;
    private BossManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindObjectsByType<BossManager>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
