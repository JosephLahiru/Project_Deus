using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growtrees : MonoBehaviour
{

    public float maxSize;
    public float growRate;
    public float scale;
    //public Color[] colorVariation;
    public float startTime;

    void Start()
    {
        maxSize = Random.Range((float)2.0, (float)6.0);
        growRate = Random.Range((float)0.2, (float)0.6);
        startTime = Time.time;

        this.transform.Rotate(0f, Random.Range(0f, 350f), 0f);
    }

    void Update()
    {
        if (scale < maxSize)
        {
            this.transform.localScale = Vector3.one * scale;
            scale += growRate * Time.deltaTime;
            //Debug.Log("Time elapsed : " + (Time.time - startTime));
        }
    }
}
