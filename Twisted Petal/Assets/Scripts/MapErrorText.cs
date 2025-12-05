using UnityEngine;

public class MapErrorText : MonoBehaviour
{
    private float timeStamp = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp < Time.time)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
    }

    public void cannotEnter()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Must equip atleast 2 weapons in inventory";
        timeStamp = Time.time + 1f;

    }
}
