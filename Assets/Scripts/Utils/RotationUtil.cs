using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;
using static Tetramino;

public static class RotationUtil
{
    // rotates each point of the poses array around rotationPoint according to the rotationType
    // and returns array with rotated points
    public static Vector2Int[] RotateArray(Vector2Int[] poses, Vector2 rotationPoint, RotationType rotationType)
    {
        int length = poses.Length;
        Vector2Int[] rotatedPoses = new Vector2Int[length];
        for (int i = 0; i < length; i++)
        {
            Vector2Int pos = poses[i];
            Vector2 rotatedPos =
                RotateVector.Rotate(pos, rotationPoint, (RotationDirection)rotationType);
            rotatedPoses[i] = VectorUtil.V2_V2Int( rotatedPos );
        }
        return rotatedPoses;
    }
}
