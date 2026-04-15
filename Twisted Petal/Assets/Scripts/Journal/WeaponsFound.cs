using System.Collections.Generic;
using UnityEngine;

public class WeaponsFound : MonoBehaviour
{
    private DataManagement saveData;
    public List<GameObject> foundWeapons;
    private bool loopDone;
    public static bool weaponsFoundHidden = true;
    public List<GameObject> weaponTypes;
    public GameObject framePrefab;
    private JournalManager journalManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foundWeapons.Clear();
        loopDone = false;
        journalManager = FindAnyObjectByType<JournalManager>();
        saveData = journalManager.gameObject.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loopDone != true)
        {
            if (saveData.ownedItems.Count <= 0)
            {
                saveData.ownedItems = journalManager.startingWeapons;
            }
            foundWeapons.Clear();
            for (int i = 0; i < saveData.ownedItems.Count; i++)
            {
                if (foundWeapons.Contains(saveData.ownedItems[i]) == false)
                {
                    foundWeapons.Add(saveData.ownedItems[i]);
                }
            }
            for (int i = 0; i < foundWeapons.Count; i++)
            {
                if (saveData.weaponsFound.Contains(foundWeapons[i]) == false)
                {
                    saveData.weaponsFound.Add(foundWeapons[i]);
                }
            }
            foundWeapons = saveData.weaponsFound;
            loopDone = true;
        }
    }
}
