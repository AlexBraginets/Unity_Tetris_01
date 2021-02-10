﻿using UnityEngine;
using CommonNonUnityUtils;
using VectorUtilLibrary;

public partial class Tetramino 
{
    public override string ToString()
    {
        string str = rotationCount + "\t" + type + "\t" + string.Join("\t", Poses);
        return str;
    }
    public static Vector2Int[] GetPoses(TetraminoType tetraminoType, RotationDirection rotationDirection)
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
    // 7 types of the tetramino
    public enum TetraminoType
    {
        I, O, T, J, L, S, Z
    }
    public enum RotationType
    {
        None, Clockwise, AntiClockwise
    }
    public TetraminoType type { get; private set; }
    // coordinates of 4 cells that make up the tetramino
    // coords are relative to the center of tetramino
    public Vector2Int[] Poses = null;
    // world position of tetramino center
    public Vector2Int centerPos;
    // point around which tetramino rotates
    public Vector2 rotationPoint;
    // each rotation clockwise increases rotation count by 1, rotation anticlockwise decreases by 1
    public int rotationCount { get; private set; } = 0;
    #region Rotation
    // increases rotation count & updates cells positions according to the rotation
    public void RotateClockwise()
    {
        rotationCount++;

        Poses = RotateArray(Poses, rotationPoint, RotationType.Clockwise);
    }
    public void Rotate(int rotationCount)
    {
        float sign = Mathf.Sign(rotationCount);
        int abs = Mathf.Abs(rotationCount);
        if (sign > 0)
        {
            this.rotationCount += abs % 4;
            LoopUtil.LoopAction((i) => RotateClockwise(), abs % 4);
        }
        else if (sign < 0)
        {
            this.rotationCount -= abs % 4;
            LoopUtil.LoopAction((i) => RotateAntiClockwise(), abs % 4);
        }
    }
    // decreases rotation count & updates cells positions according to the rotation
    public void RotateAntiClockwise()
    {
        rotationCount--;
        Poses = RotateArray(Poses, rotationPoint, RotationType.AntiClockwise);

    }
    // rotates each point of the poses array around rotationPoint according to the rotationType
    // and returns array with rotated points
    public static Vector2Int[] RotateArray(Vector2Int[] poses, Vector2 rotationPoint, RotationType rotationType)
    {
        int length = poses.Length;
        Vector2Int[] rotatedPoses = new Vector2Int[length];
        for(int i = 0; i < length; i++)
        {
            Vector2Int pos = poses[i];
            Vector2Int rotatedPos = Vector2Int.zero;
            Vector2 newPos;
            switch (rotationType)
            {
                case RotationType.None:
                    rotatedPos = pos;
                    break;
                case RotationType.Clockwise:
                    newPos = RotateVector.Rotate(pos, rotationPoint, RotationDirection.Clockwise);
                    rotatedPos = VectorUtil.V2_V2Int(newPos);
                    break;
                case RotationType.AntiClockwise:
                    newPos = RotateVector.Rotate(pos, rotationPoint, RotationDirection.CounterClockwise);
                    rotatedPos = VectorUtil.V2_V2Int(newPos);
                    break;
                default:
                    Debug.LogError("this rotation type is not supported: " + rotationType);
                    break;
            }
            rotatedPoses[i] = rotatedPos;
        }
        return rotatedPoses;
    }
    #endregion
    #region Poses: AbsPoses property and GetAbsPoses method
    // returns WORLD cells coordinates
    public Vector2Int[] AbsPoses
    {
        get
        {
            return VectorUtil.Add(Poses, centerPos);
        }
    }
    // returns WORLDS cells coordinates if the tetramino was moved by offset & rotated according to rotation type
    public Vector2Int[] GetAbsPoses(Vector2Int offset, RotationType rotation)
    {
        Vector2Int[] rotated = RotateArray(Poses, rotationPoint, rotation);
        return VectorUtil.Add(rotated, centerPos + offset);
    }
    #endregion
    // set ups rotation point and coordinates(relative to the center of tetramino) of cells that form a tetromino
    public Tetramino(TetraminoType type)
    {
        Builder.SetUp(this, type);
    }
    
}
