using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TetraminoTransformUtil
{
    public static Vector2Int[] GetPoses(Tetramino.TetraminoType tetraminoType, RotationDirection rotationDirection)
    {
        Tetramino tetramino = new Tetramino(tetraminoType);
        switch (rotationDirection)
        {
            case RotationDirection.None:
                break;
            case RotationDirection.Clockwise:
                tetramino.RotateClockwise();
                break;
            case RotationDirection.CounterClockwise:
                tetramino.RotateAntiClockwise();
                break;
            default:
                throw new UnityException($"RotationDirection type<{rotationDirection}> not supported!");
        }
        return tetramino.Poses;
    }
}
