using System.Collections;
using System.Collections.Generic;
using TransformUtilLibrary;
using UnityEngine;

public class TestTransformUtil_GetChildren : MonoBehaviour
{

    public Transform[] children;
    [ContextMenu("GetChildren" )]
    public void GetChildren()
    {
        children = TransformUtil.GetChildren(transform);
    }
}
