using UnityEngine;
using UnityEngine.UI;

public class BarBehavior : MonoBehaviour
{
    [Header("Bar Values")]
    public float maxValue;
    public float minValue;
    public float startingValue;
    public float value;

    [Header("Bar Config")]
    public bool doGreenToRedTransition;
    public enum ScaleLoc
    {
        UpDown,
        LeftRight
    }
    public ScaleLoc barScaleLocation;
    public float barSizeX;
    public float barSizeY;
    private RectTransform barTransform;
    public Vector2 startPos;

    public GameObject edgeObject;
    public float currentSizeDeltaX;
    public float currentSizeDeltaY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barTransform = gameObject.GetComponent<RectTransform>();
        startPos = barTransform.localPosition;
        barSizeX = barTransform.localScale.x;
        barSizeY = barTransform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentSizeDeltaX = barTransform.localScale.x;
        currentSizeDeltaY = barTransform.localScale.y;

        // scale the bar as directed
        if (barScaleLocation == ScaleLoc.UpDown)
        {
            // scale from middle on the y axis
            barTransform.localScale = new Vector2(barSizeX, (value / maxValue) * barSizeY);
        }
        else if (barScaleLocation == ScaleLoc.LeftRight)
        {
            // scale from middle on the x axis
            barTransform.localScale = new Vector2((value / maxValue) * barSizeX, barSizeY);
        }
        
        if (doGreenToRedTransition)
        {
            gameObject.GetComponent<Image>().color = Color.HSVToRGB(((value / maxValue) * 130f) / 360f, 1f, 1f);
        }

        if (edgeObject != null)
        {
            edgeObject.transform.localPosition = new Vector2(barTransform.sizeDelta.x, 0);
        }
        
    }
}
