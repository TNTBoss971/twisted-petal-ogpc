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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foundWeapons.Clear();
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponsFoundHidden == true)
        {
            loopDone = false;
        }
        else
        {
            if (loopDone != true)
            {
                foundWeapons.Clear();
                for (int i = 0; i < saveData.ownedItems.Count; i++)
                {
                    if (foundWeapons.Contains(saveData.ownedItems[i]) == false)
                    {
                        foundWeapons.Add(saveData.ownedItems[i]);
                    }
                }
                loopDone = true;
            }
        }
    }
}
