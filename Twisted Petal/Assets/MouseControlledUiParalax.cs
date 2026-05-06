using UnityEngine;

public class MouseControlledUiParalax : MonoBehaviour
{
    public Vector2 origin;
    public float intensity;

    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse position
        Vector2 mousePos = Input.mousePosition;
        /*
        mouseScreenPosition.z = cam.nearClipPlane;
        Vector2 mousePosInWorld = cam.ScreenToWorldPoint(mouseScreenPosition);*/

        // get direction away from mouse
        Vector2 direction = origin - mousePos;
        Vector2 distance = direction * intensity;
        transform.position = origin + distance;

    }
}
