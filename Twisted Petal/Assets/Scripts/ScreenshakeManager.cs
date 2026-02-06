using UnityEngine;

public class ScreenshakeManager : MonoBehaviour
{
    public ScreenshakeCaller[] callers;
    private bool shakeOn;
    public Camera cam;
    public GameObject camObject;
    public float magnitude;
    public float frequency;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shakeOn = false;
        callers = Object.FindObjectsByType<ScreenshakeCaller>(FindObjectsSortMode.None);
        for(int i = 0; i < callers.Length; i++)
        {
            if (callers[i].screenshakeOn)
            {
                shakeOn = true;
                break;
            }
        }
        if (shakeOn)
        {
            float offset = Mathf.Sin((Time.time + magnitude) * frequency) * magnitude;
            camObject.transform.position = new Vector3(offset, camObject.transform.position.y, camObject.transform.position.z);
        }
        else
        {
            camObject.transform.position = new Vector3(0, camObject.transform.position.y, camObject.transform.position.z);
        }
    }
}
