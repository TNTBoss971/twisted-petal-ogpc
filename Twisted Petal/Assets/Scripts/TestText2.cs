using UnityEngine;

public class TestText2 : MonoBehaviour, IDataPersistance
{
    public static int testingVariableTwo = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LoadData(GameData data)
    {
        testingVariableTwo = data.testVarTwo;
    }

    public void SaveData(ref GameData data)
    {
        data.testVarTwo = testingVariableTwo;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Variable 2: " + testingVariableTwo;
    }
}
