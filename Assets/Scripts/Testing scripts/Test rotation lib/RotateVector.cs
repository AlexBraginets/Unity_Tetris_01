using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RotationDirection
{
    None,
    Clockwise,
    CounterClockwise
}

public static class RotateVector
{
    public static Vector2 Rotate(Vector2 vector, RotationDirection rotation)
    {
        Vector2 rotated;
        switch (rotation)
        {
            case RotationDirection.None:
                rotated = vector;
                break;
            case RotationDirection.Clockwise:
                rotated = RotateClockwise(vector);
                break;
            case RotationDirection.CounterClockwise:
                rotated = RotateCounterClockwise(vector);
                break;
            default:
                throw new UnityException($"RotationDirection type<{rotation}> not supported!");
        }
        return rotated;
    }
    private static Vector2 RotateClockwise(Vector2 v)
    {
        return new Vector2(v.y, -v.x);
    }
    private static Vector2 RotateCounterClockwise(Vector2 v)
    {
        return new Vector2(-v.y, v.x);
    }

}
