using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtil
{
    public static T[] GetValues<T>()
    {
        return (T[])System.Enum.GetValues(typeof(T));
    }
}
