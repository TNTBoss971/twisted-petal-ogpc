using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static float enemyNumber;
    public static float waveNumber = 0f;
    public GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyNumber == 0)
        {
            waveNumber += 1;
            for (int i = 0; i < 4; i++)
            {
                GameObject clone;
                clone = Instantiate(enemy, new Vector2(5, -1), transform.rotation);
                enemyNumber++;
            }
        }
    }
}
