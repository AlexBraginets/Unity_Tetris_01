using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Tetramino
{
    public static class Builder
    {
       public static Tetramino SetUp(Tetramino tetramino, Tetramino.TetraminoType type)
        {
            tetramino.type = type;
            switch (type)
            {
                case TetraminoType.I:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } });
                    tetramino.rotationPoint = new Vector2(3f / 2f, -1f / 2f);
                    break;
                case TetraminoType.J:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 1, 2 } });
                    tetramino.rotationPoint = new Vector2(1f, 1f);
                    break;
                case TetraminoType.L:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 0, 2 } });
                    tetramino.rotationPoint = new Vector2(0f, 1f);
                    break;
                case TetraminoType.O:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } });
                    tetramino.rotationPoint = new Vector2(1f / 2f, 1f / 2f);
                    break;
                case TetraminoType.S:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 2, 1 } });
                    tetramino.rotationPoint = new Vector2(1f, 0f);
                    break;
                case TetraminoType.T:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } });
                    tetramino.rotationPoint = new Vector2(1f, 1f);
                    break;
                case TetraminoType.Z:
                    tetramino.Poses = GetPoses(new int[4, 2] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 2, 0 } });
                    tetramino.rotationPoint = new Vector2(1f, 0f);
                    break;
                default:
                    Debug.LogError("Unsupported tetramino type: " + type);
                    break;
            }
            return tetramino;
        }
        // utility for initializing Poses array
        private static Vector2Int[] GetPoses(int[,] doubleArrayPoses)
        {
            Vector2Int[] vectorPoses = new Vector2Int[4];
            if (doubleArrayPoses.GetLength(0) != 4 || doubleArrayPoses.GetLength(1) != 2)
            {
                Debug.LogError("Invalid double array poses!");
            }
            for (int posIndex = 0; posIndex < 4; posIndex++)
            {
                int x = doubleArrayPoses[posIndex, 0];
                int y = doubleArrayPoses[posIndex, 1];
                vectorPoses[posIndex] = new Vector2Int(x, y);
            }
            return vectorPoses;
        }
    }

}
