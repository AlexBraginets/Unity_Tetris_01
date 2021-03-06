﻿using UnityEngine;
[System.Serializable]
public class Tetramino
{
    // 7 types of the tetramino
    public enum TetraminoType
    {
        I, O, T, J, L, S, Z
    }
    public enum RotationType
    {
        None, Clockwise, AntiClockwise
    }
    public TetraminoType type { get; }
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
        for(int i = 0; i < 4; i++)
        {
            Poses[i] = RotateClockwise(Poses[i], rotationPoint);
        }
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
        for (int i = 0; i < 4; i++)
        {
            Poses[i] = RotateAntiClockwise(Poses[i], rotationPoint);
        }
    }
    // rotates(clockwise)  point( with pos coordinates) around center
    public static Vector2Int RotateClockwise(Vector2Int pos, Vector2 center)
    {
        Vector2 posRaw = ((Vector2)pos) - center;
        float x = posRaw.y;
        float y = -posRaw.x;
        Vector2 newPos = new Vector2(x, y) + center;
        return new Vector2Int((int)newPos.x, (int)newPos.y);
    }
    // rotates(anticlockwise)  point( with pos coordinates) around center
    public static Vector2Int RotateAntiClockwise(Vector2Int pos, Vector2 center)
    {
        Vector2 posRaw = ((Vector2)pos) - center;
        float x = -posRaw.y;
        float y = posRaw.x;
        Vector2 newPos = new Vector2(x, y) + center;
        return new Vector2Int((int)newPos.x, (int)newPos.y);
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
            switch (rotationType)
            {
                case RotationType.None:
                    rotatedPos = pos;
                    break;
                case RotationType.Clockwise:
                    rotatedPos = RotateClockwise(pos, rotationPoint);
                    break;
                case RotationType.AntiClockwise:
                    rotatedPos = RotateAntiClockwise(pos, rotationPoint);
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
        this.type = type;
        switch (type)
        {
            case TetraminoType.I:
                Poses = GetPoses(new int[4,2]{ {0, 0},{1, 0},{2, 0},{3, 0} });
                rotationPoint = new Vector2(3f/2f, -1f/2f);
                break;
            case TetraminoType.J:
                Poses = GetPoses(new int[4, 2] { {0, 0}, {1 , 0}, {1, 1}, {1, 2} });
                rotationPoint = new Vector2(1f, 1f);
                break;
            case TetraminoType.L:
                Poses = GetPoses(new int[4, 2] { {0, 0}, {1, 0}, {0, 1}, {0, 2} });
                rotationPoint = new Vector2(0f, 1f);
                break;
            case TetraminoType.O:
                Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } });
                rotationPoint = new Vector2(1f/2f, 1f/2f);
                break;
            case TetraminoType.S:
                Poses = GetPoses(new int[4, 2] { {0, 0}, {1 , 0}, {1, 1}, {2, 1} });
                rotationPoint = new Vector2(1f, 0f);
                break;
            case TetraminoType.T:
                Poses = GetPoses(new int[4, 2] { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } });
                rotationPoint = new Vector2(1f, 1f);
                break;
            case TetraminoType.Z:
                Poses = GetPoses(new int[4, 2] { {0, 1}, {1, 1}, {1, 0}, {2, 0} });
                rotationPoint = new Vector2(1f, 0f);
                break;
            default:
                Debug.LogError("Unsupported tetramino type: " + type);
                break;
        }
    }

    // utility for initializing Poses array
    private static Vector2Int[] GetPoses(int[,] doubleArrayPoses)
    {
        Vector2Int[] vectorPoses = new Vector2Int[4];
        if(doubleArrayPoses.GetLength(0)!=4 || doubleArrayPoses.GetLength(1)!= 2)
        {
            Debug.LogError("Invalid double array poses!");
        }
        for(int posIndex = 0; posIndex < 4; posIndex++)
        {
            int x = doubleArrayPoses[posIndex, 0];
            int y = doubleArrayPoses[posIndex, 1];
            vectorPoses[posIndex] = new Vector2Int(x, y);
        }
        return vectorPoses;
    }
}
