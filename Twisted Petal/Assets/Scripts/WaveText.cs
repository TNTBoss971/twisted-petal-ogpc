using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveText : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Wave: " + EnemyManager.waveNumber;
    }
}