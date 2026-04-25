using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGround : MonoBehaviour
{
    public float speed;
    public float startingPos;
    public GameObject road0;
    public float road0X;
    public GameObject road1;
    public float road1X;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        road1.transform.position = new Vector2(startingPos, road1.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        road0X = road0.transform.position.x;
        road1X = road1.transform.position.x;

        road0X-= speed*Time.deltaTime;
        if (road0X <= -startingPos)
        {
            road0X = startingPos;
        }

        road1X-= speed*Time.deltaTime;
        if (road1X <= -startingPos)
        {
            road1X = startingPos;
        }
        
        road0.transform.position = new Vector3(road0X, road0.transform.position.y, transform.position.z);
        road1.transform.position = new Vector3(road1X, road1.transform.position.y, transform.position.z);
    }
}
