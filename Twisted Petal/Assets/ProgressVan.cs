using UnityEngine;

public class ProgressVan : MonoBehaviour
{
    public GameManagement gameManagemer;
    public Camera cam;
    public float leftX;
    public float rightX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightX = cam.scaledPixelWidth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(rightX * Time.time / gameManagemer.waveLength, transform.position.y);
    }
}
