using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransformAssignment : MonoBehaviour
{
    private void Update()
    {
        transform.position = Vector3.zero;
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = Vector3.one;
        }
        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log(transform.position);
        }
    }
}
