using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour, IDataPersistance
{
    public static int enemyNumber;
    public static int waveNumber;
    public GameObject enemy;
    public DataPersistanceManager dataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveNumber = 0;
        enemyNumber = 0;
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

        if (waveNumber == 4)
        {
            MapManager.levelsBeaten += 1;
            dataManager.SaveGame();
            Debug.Log(MapManager.levelsBeaten);
            SceneManager.LoadScene("WorldMap");
        }
    }

    public void LoadData(GameData data)
    {
        MapManager.levelsBeaten = data.levelsBeaten;
    }

    public void SaveData(ref GameData data)
    {
        data.levelsBeaten = MapManager.levelsBeaten;
    }
}
