using UnityEngine;

public class ItemPopup : MonoBehaviour
{
    public bool showPopup;
    private float popupTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (popupTimer <= Time.time)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
    }

    public void displayPopup(string popupText)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = popupText;
        showPopup = true;
        popupTimer = Time.time + 1.5f;
    }
}
