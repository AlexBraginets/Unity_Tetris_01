using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;
using static RotationUtil;

public class TetraminoTransform
{
    public Tetramino.TetraminoType type { get; protected set; }
    // coordinates of 4 cells that make up the tetramino
    // coords are relative to the center of tetramino
    public Vector2Int[] Poses = null;
    // world position of tetramino center
    public Vector2Int centerPos;
    // point around which tetramino rotates
    public Vector2 rotationPoint;
    // each rotation clockwise increases rotation count by 1, rotation anticlockwise decreases by 1
    public int rotationCount { get; protected set; } = 0;
   
    // returns WORLDS cells coordinates if the tetramino was moved by offset & rotated according to rotation type
    public Vector2Int[] GetAbsPoses(Vector2Int offset, Tetramino.RotationType rotation)
    {
        Vector2Int[] rotated = RotateArray(Poses, rotationPoint, rotation);
        return VectorUtil.Add(rotated, centerPos + offset);
    }
    // returns WORLD cells coordinates
    public Vector2Int[] AbsPoses
    {
        get
        {
            return VectorUtil.Add(Poses, centerPos);
        }
    }
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
