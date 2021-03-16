using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;
public class TestingVectorRotate : MonoBehaviour
{
    [ContextMenu("Full test")]
    public void FullTest()
    {
        Tetramino.TetraminoType[] tetraminoTypes =
          EnumUtil.GetValues<Tetramino.TetraminoType>();
        RotationDirection[] rotationDirectionTypes =
            EnumUtil.GetValues<RotationDirection>();
        bool identic = true;
        int testCount = 0;
        foreach(var tetraminoType in tetraminoTypes)
        {
            foreach(var rotationDirection in rotationDirectionTypes)
            {
                string rotated_string = VectorPosesString(tetraminoType, rotationDirection);
                string tetramino_string = TetraminoString(tetraminoType, rotationDirection);
                if (string.Compare(rotated_string, tetramino_string)!=0)
                {
                    identic = false;
                    break;
                }
                testCount++;
            }
        }
        Debug.Log($"identic: {identic}");
        Debug.Log($"test count: {testCount}");
    }
    //[SerializeField] RotationDirection rotationDirection;
    //[SerializeField] Tetramino.TetraminoType tetraminoType;
    //[ContextMenu("Test")]
    //public void Test()
    //{
    //    Vector2Int[] tetraminoRotatedPoses = Tetramino.GetPoses(tetraminoType, rotationDirection);
    //    Vector2Int[] tetraminoPoses = Tetramino.GetPoses(tetraminoType, RotationDirection.None);
    //    Vector2Int[] rotationVectorPoses = new Vector2Int[4];
    //    Vector2 rotationPoint = new Tetramino(tetraminoType).rotationPoint;
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Vector2 rotated = RotateVector.Rotate(tetraminoPoses[i] - rotationPoint, rotationDirection);
    //        rotationVectorPoses[i] = VectorUtil.V2_V2Int(rotated + rotationPoint);
    //    }
    //    Debug.Log("RotateVector:");
    //    Debug.Log(string.Join("\t", rotationVectorPoses));
    //    Debug.Log("TetraminoRotate:");
    //    Debug.Log(string.Join("\t", tetraminoRotatedPoses));
    //}
    private static string TetraminoString(Tetramino.TetraminoType tetraminoType, RotationDirection rotationDirection)
    {
        Vector2Int[] tetraminoRotatedPoses = TetraminoTransformUtil.GetPoses(tetraminoType, rotationDirection);
        return string.Join("\t", tetraminoRotatedPoses);
    }
    private static string VectorPosesString(Tetramino.TetraminoType tetraminoType, RotationDirection rotationDirection)
    {
        Vector2Int[] tetraminoPoses = TetraminoTransformUtil.GetPoses(tetraminoType, RotationDirection.None);
        Vector2Int[] rotationVectorPoses = new Vector2Int[4];
        Vector2 rotationPoint = new Tetramino(tetraminoType).rotationPoint;
        for (int i = 0; i < 4; i++)
        {
            Vector2 rotated = RotateVector.Rotate(tetraminoPoses[i], rotationPoint, rotationDirection);
            rotationVectorPoses[i] = VectorUtil.V2_V2Int(rotated);
        }
        return string.Join("\t", rotationVectorPoses);
    }
}
