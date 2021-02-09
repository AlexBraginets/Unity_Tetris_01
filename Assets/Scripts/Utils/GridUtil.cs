using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtil
{
    public static Vector2 PositionFromIndex(int index, int gridWidth)
    {
        int x = index % gridWidth;
        int y = -(index / gridWidth);
        return new Vector2(x, y);
    }
}
