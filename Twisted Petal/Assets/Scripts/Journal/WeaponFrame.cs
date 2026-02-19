using UnityEngine;
using UnityEngine.UI;

public class WeaponFrame : MonoBehaviour
{
    public GameObject itemStored;
    private Sprite frameImage;
    private JournalManager journalManager;
    private DataManagement saveData;
    private bool loopDone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frameImage = itemStored.GetComponent<GunController>().displayImage;
        journalManager = FindAnyObjectByType<JournalManager>();
        saveData = journalManager.GetComponent<DataManagement>();
        loopDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loopDone == false)
        {
            if (saveData.ownedItems.Contains(itemStored))
            {
                this.GetComponent<Image>().sprite = frameImage;
            }
            loopDone = true;
        }
        if (WeaponsFound.weaponsFoundHidden == true)
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
