using UnityEngine;

public class DataManagement : MonoBehaviour, IDataPersistance
{
    public int levelsBeaten = 0;
    public GameObject[] equipedWeapons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadData(GameData data)
    {
        levelsBeaten = data.levelsBeaten;
    }

    public void SaveData(ref GameData data)
    {
        data.levelsBeaten = levelsBeaten;
    }
}
