﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMyComponent : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<MyComponent>();   
    }
    
}
