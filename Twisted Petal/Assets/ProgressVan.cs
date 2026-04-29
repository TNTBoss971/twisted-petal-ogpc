using UnityEngine;

public class ProgressVan : MonoBehaviour
{
    public GameManagement gameManagemer;
    public Camera cam;
    public float leftX;
    public float rightX;
    private float startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightX = cam.scaledPixelWidth;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagemer.currentWave.isBossBattle)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector2(rightX * (Time.time - startTime) / gameManagemer.waveLength, transform.position.y);
    }
}
