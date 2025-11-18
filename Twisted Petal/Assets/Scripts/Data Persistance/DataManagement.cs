using UnityEngine;
using System.Collections.Generic;

public class DataManagement : MonoBehaviour, IDataPersistance
{
    public int levelsBeaten = 0;
    public List<GameObject> selectedItems;
    public List<GameObject> weaponTypes;
    
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
        selectedItems = data.selectedItems;
        weaponTypes = data.weaponTypes;
    }

    public void SaveData(ref GameData data)
    {
        data.levelsBeaten = levelsBeaten;
        data.selectedItems = selectedItems;
        data.weaponTypes = weaponTypes;
    }
}
