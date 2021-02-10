using UnityEngine;
using CommonNonUnityUtils;
using VectorUtilLibrary;
using static RotationUtil;
public partial class Tetramino : TetraminoTransform
{
    // set ups rotation point and coordinates(relative to the center of tetramino) of cells that form a tetromino
    public Tetramino(TetraminoType type)
    {
        Builder.SetUp(this, type);
    }
    public override string ToString()
    {
        string str = rotationCount + "\t" + type + "\t" + string.Join("\t", Poses);
        return str;
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
    
    #endregion
   
    
}
