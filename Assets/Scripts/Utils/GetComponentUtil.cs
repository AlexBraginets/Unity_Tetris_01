using CommonNonUnityUtils;
using System.Collections;
using System.Collections.Generic;
using TransformUtilLibrary;
using UnityEngine;

public static class GetComponentUtil
{
   public static T[] GetComponentsInChildren<T>(Transform transform, out int childCount)
    {
        Transform[] children = TransformUtil.GetChildren(transform);
        childCount = children.Length;
        T[] components =
            LoopUtil.LoopFunc<T>((i) => children[i].GetComponent<T>(), childCount);
        return components;
    }
}
