using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static int mapPosition = 1;
    private float moveCooldown = 0f;
    private DataManagement saveData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData = this.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pressing d on the map moves you forward as long as you have beaten enough levels.
        if (Input.GetKey("d"))
        {
            if (mapPosition != 7)
            {
                if (moveCooldown <= Time.time)
                {
                    if (saveData.levelsBeaten <= mapPosition - 1)
                    {

                    }
                    else
                    {
                        mapPosition += 1;
                        moveCooldown = Time.time + 0.2f;
                    }
                }
            }
        }

        // Pressing a on the map moves you back as long as you don't go past level 1
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

        // Pressing enter on the map takes you into a level
        if (Input.GetKey("return"))
        {
            SceneManager.LoadScene("Combat");
        }
    }
}
