using System.Collections.Generic;
using UnityEngine;

public class WeaponsFound : MonoBehaviour
{
    private DataManagement saveData;
    public List<GameObject> foundWeapons;
    private bool loopDone;
    public static bool weaponsFoundHidden = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foundWeapons.Clear();
        saveData = this.GetComponent<DataManagement>();
        GetComponent<TMPro.TextMeshProUGUI>().text = "Weapons Discovered: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponsFoundHidden == true)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
            loopDone = false;
        }
        else
        {
            if (loopDone != true)
            {
                foundWeapons.Clear();
                GetComponent<TMPro.TextMeshProUGUI>().text = "Weapons Discovered: ";
                for (int i = 0; i < saveData.ownedItems.Count; i++)
                {
                    if (foundWeapons.Contains(saveData.ownedItems[i]) == false)
                    {
                        foundWeapons.Add(saveData.ownedItems[i]);
                        GetComponent<TMPro.TextMeshProUGUI>().text += saveData.ownedItems[i]
                        .GetComponent<GunController>().weaponName + ", ";
                    }
                }
                loopDone = true;
            }
        }
    }
}
