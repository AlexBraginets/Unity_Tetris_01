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
    public Vector2Int[] Poses { get; protected set; } = null;
    // world position of tetramino center
    public Vector2Int centerPos { get; protected set; }
    // point around which tetramino rotates
    public Vector2 rotationPoint { get;protected set; }
    // each rotation clockwise increases rotation count by 1, rotation anticlockwise decreases by 1
    public int rotationCount { get; protected set; } = 0;
    // returns cells' WORLD coordinates if the tetramino was moved by offset & rotated according to rotation type
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
            return GetAbsPoses(Vector2Int.zero, Tetramino.RotationType.None);
        }
    }
    public void SetCenterPosition(Vector2Int centerPos)
    {
        this.centerPos = centerPos;
    }

}
