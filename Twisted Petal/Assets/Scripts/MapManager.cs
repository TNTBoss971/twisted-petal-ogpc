using UnityEngine;

public class MapManager : MonoBehaviour, IDataPersistance
{
    public static int mapPosition = 1;
    private float moveCooldown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            if (mapPosition != 7)
            {
                if (moveCooldown <= Time.time)
                {
                    mapPosition += 1;
                    moveCooldown = Time.time + 0.2f;
                }
            }
        }

        if (Input.GetKey("a"))
        {
            if (mapPosition != 1)
            {
                if (moveCooldown <= Time.time)
                {
                    mapPosition -= 1;
                    moveCooldown = Time.time + 0.2f;
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        mapPosition = data.testVarOne;
    }

    public void SaveData(ref GameData data)
    {
        data.testVarOne = mapPosition;
    }
}
