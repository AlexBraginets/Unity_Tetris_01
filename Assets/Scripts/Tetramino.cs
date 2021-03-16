using UnityEngine;
using CommonNonUnityUtils;
using VectorUtilLibrary;
using static RotationUtil;
public partial class Tetramino : TetraminoTransform
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
    // sets up rotation point and tetramino's cells coordinates
    // also sets up tetramino's type
    public Tetramino(TetraminoType type)
    {
        Builder.SetUp(this, type);
    }
    #region Rotation
    // increases rotation count & updates cells positions according to the rotation
    public void RotateClockwise()
    {
        rotationCount++;
        Poses = RotateArray(Poses, rotationPoint, RotationType.Clockwise);
    }
    // decreases rotation count & updates cells positions according to the rotation
    public void RotateAntiClockwise()
    {
        rotationCount--;
        Poses = RotateArray(Poses, rotationPoint, RotationType.AntiClockwise);

    }
    public void Rotate(int rotationCount)
    {
        float sign = Mathf.Sign(rotationCount);
        int abs = Mathf.Abs(rotationCount);
        rotationCount = abs % 4;
        if (sign > 0)
        {
            LoopUtil.LoopAction((i) => RotateClockwise(), rotationCount);
        }
        else if (sign < 0)
        {
            LoopUtil.LoopAction((i) => RotateAntiClockwise(), rotationCount);
        }
    }
    #endregion
    // returns tetramino's rotation count,  type and local  positions
    public override string ToString()
    {
        return rotationCount + "\t" + type + "\t" + string.Join("\t", Poses);
    }

}
