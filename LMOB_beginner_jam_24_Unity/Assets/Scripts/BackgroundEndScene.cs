﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * 1f * Time.fixedDeltaTime);
    }
}
