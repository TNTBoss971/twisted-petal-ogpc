using UnityEngine;

public class TestText2 : MonoBehaviour
{
    public static int testingVariableTwo = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Variable 2: " + testingVariableTwo;
    }
}
