using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonSelectSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Lets the button know when it is pressed
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TaskOnClick()
    {
        audioSource.Play();
    }
}
