using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMapIcon : MonoBehaviour
{
    // Public vars for positions
    public float pos1X;
    public float pos1Y;
    public float pos2X;
    public float pos2Y;
    public float pos3X;
    public float pos3Y;
    public float pos4X;
    public float pos4Y;
    public float pos5X;
    public float pos5Y;
    public float pos6X;
    public float pos6Y;
    public float pos7X;
    public float pos7Y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MapManager.mapPosition == 1)
        {
            transform.position = new Vector2(pos1X, pos1Y);
        }

        if (MapManager.mapPosition == 2)
        {
            transform.position = new Vector2(pos2X, pos2Y);
        }

        if (MapManager.mapPosition == 3)
        {
            transform.position = new Vector2(pos3X, pos3Y);
        }

        if (MapManager.mapPosition == 4)
        {
            transform.position = new Vector2(pos4X, pos4Y);
        }

        if (MapManager.mapPosition == 5)
        {
            transform.position = new Vector2(pos5X, pos5Y);
        }

        if (MapManager.mapPosition == 6)
        {
            transform.position = new Vector2(pos6X, pos6Y);
        }

        if (MapManager.mapPosition == 7)
        {
            transform.position = new Vector2(pos7X, pos7Y);
        }
    }
}
