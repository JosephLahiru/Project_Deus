using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    void Start()
    {
        this.transform.Rotate(0f, Random.Range(0f, 350f), 0f);
    }

    void Update()
    {
        
    }
}
