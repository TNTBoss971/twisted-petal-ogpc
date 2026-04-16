using UnityEngine;

public class WaveData : MonoBehaviour
{
    public bool isBossBattle;
    public float length;
    public int maxEnemies;
    public float spawnrate;
    public GameObject[] enemiesInWave;
    public GameObject[] weaponsInWave;
    [Header("All frequencies must individually add up to 1")]
    public float[] enemyFrequency;
    public float[] weaponFrequency;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
