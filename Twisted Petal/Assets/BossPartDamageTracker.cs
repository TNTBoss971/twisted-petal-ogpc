using UnityEngine;

public class BossPartDamageTracker : MonoBehaviour
{
    private float totalDamage;
    public float damageThisAttack;
    public BossManager manager;
    public bool isWeakPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindObjectsByType<BossManager>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamageSelf(float damage)
    {
        totalDamage += damage;
        damageThisAttack += damage;
        if (isWeakPoint)
        {
            manager.health -= damage;
        } 
        else
        {
            manager.health -= damage / 2;
        }
    }
}
