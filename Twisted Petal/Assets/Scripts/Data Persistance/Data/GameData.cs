using UnityEngine;

[System.Serializable]
public class GameData
{
    public int testVarOne;
    public int testVarTwo;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load

    public GameData()
    {
        this.testVarOne = 0;
        this.testVarTwo = 0;
    }
}
