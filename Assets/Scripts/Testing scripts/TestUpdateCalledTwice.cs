using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpdateCalledTwice : MonoBehaviour
{
    
    void Update()
    {
        Debug.Log(Time.frameCount);
    }
}
