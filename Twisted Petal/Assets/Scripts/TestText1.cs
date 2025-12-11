using UnityEngine;

public class TestText1 : MonoBehaviour, IDataPersistance
{
    public static int testingVariableOne = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LoadData(GameData data)
    {
        testingVariableOne = data.testVarOne;
    }

    public void SaveData(ref GameData data)
    {
        data.testVarOne = testingVariableOne;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Variable 1: " + testingVariableOne;
    }
}
