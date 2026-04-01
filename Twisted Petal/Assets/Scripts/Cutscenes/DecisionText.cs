using UnityEngine;

public class DecisionText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // set text to buttonText variable of parent
        GetComponent<TMPro.TextMeshProUGUI>().text = GetComponentInParent<CutsceneDecisionButton>().buttonText;
    }
}
