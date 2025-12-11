using UnityEngine;
using UnityEngine.UI;

public class SlotDisplayLogic : MonoBehaviour
{
    public Sprite displayImage;
    public string displayName;
    public string displayDiscription;

    public Image imageObject;
    public TMPro.TextMeshProUGUI nameObject;
    public TMPro.TextMeshProUGUI discriptionObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        imageObject.sprite = displayImage;
        nameObject.text = displayName;
        discriptionObject.text = displayDiscription;
    }
}
