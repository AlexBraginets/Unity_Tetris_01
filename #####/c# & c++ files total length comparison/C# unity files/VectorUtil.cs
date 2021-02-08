using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtil
{
    public static Vector2Int[] Add(Vector2Int[] vectorArray, Vector2Int toAdd)
    {
        int length = vectorArray.Length;
        Vector2Int[] newArray = new Vector2Int[length];
        for(int i = 0; i < length; i++)
        {
            newArray[i] = vectorArray[i] + toAdd;
        }
        return newArray;
    }
    public static Vector2[] Multiply(Vector2Int[] vectorArray, float multiplier)
    {
        int length = vectorArray.Length;
        return LoopUtil.LoopFunc<Vector2>((i)=>(Vector2)vectorArray[i]*multiplier, length);
    }
    public static Vector2Int V2_V2Int(Vector2 vector2)
    {
        return new Vector2Int((int)vector2.x, (int)vector2.y);
    }
    public static Vector2Int V3_V2Int(Vector3 vector3)
    {
        return new Vector2Int((int)vector3.x, (int)vector3.y);
    }
}
