using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float mouse_x;
    public float mouse_y;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = Object.FindObjectsByType<GameManager>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        mouse_x = gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        mouse_y = gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
        gameObject.transform.position = new Vector2(mouse_x, mouse_y);
    }
}
