using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static int mapPosition = 1;
    private DataManagement saveData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        mapPosition = saveData.levelsBeaten + 1;
        if (mapPosition > 7)
        {
            mapPosition = 7;
        }

        // Pressing enter on the map takes you into a level
        if (Input.GetKey("return"))
        {
            SceneManager.LoadScene("Combat");
        }
    }
}
