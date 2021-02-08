using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil
{
    public static Color ColorWithOpacity(Color color, float opacity)
    {
        return new Color(color.r, color.g, color.b, opacity);
    }
}
