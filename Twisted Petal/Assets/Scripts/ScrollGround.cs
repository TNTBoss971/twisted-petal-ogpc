using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGround : MonoBehaviour
{
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField]
    private Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset+= new Vector2(speed*Time.deltaTime, 0);
    }
}
