using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtil
{
    public static Transform[] GetChildren(Transform transform)
    {
        int childCount = transform.childCount;
        return LoopUtil.LoopFunc<Transform>((i)=>transform.GetChild(i), childCount);
    }
    public static void ApplyLocalPoses(Transform[] transforms, Vector2[] poses)
    {
        if(transforms.Length != poses.Length)
        {
            Debug.Log(transforms.Length + " : " + poses.Length);
            throw new System.Exception("transforms.Length != poses.Length");
        }
        LoopUtil.LoopAction((i) => transforms[i].localPosition = poses[i], transforms.Length);
    }
}
