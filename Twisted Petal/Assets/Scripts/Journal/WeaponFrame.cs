using UnityEngine;
using UnityEngine.UI;

public class WeaponFrame : MonoBehaviour
{
    public GameObject itemStored;
    private Sprite frameImage;
    private JournalManager journalManager;
    private DataManagement saveData;
    private bool loopDone;
    private WeaponsFound weaponsFound;
    public GameObject placeholderImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frameImage = itemStored.GetComponent<GunController>().displayImage;
        journalManager = FindAnyObjectByType<JournalManager>();
        saveData = journalManager.GetComponent<DataManagement>();
        loopDone = false;
        weaponsFound = FindAnyObjectByType<WeaponsFound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loopDone == false)
        {
            if (weaponsFound.foundWeapons.Contains(itemStored))
            {
                this.GetComponent<Image>().sprite = frameImage;
            }
            else
            {
                this.GetComponent<Image>().sprite = placeholderImage.GetComponent<SpriteRenderer>().sprite;
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
