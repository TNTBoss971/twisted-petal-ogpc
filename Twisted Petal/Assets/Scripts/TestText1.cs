using UnityEngine;

public class TestText1 : MonoBehaviour
{
    public static int testingVariableOne = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Variable 1: " + testingVariableOne;
    }
}
