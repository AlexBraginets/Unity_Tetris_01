using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TetraminoUtil
{
    private static System.Random rnd = new System.Random();
    public static Tetramino.TetraminoType RandomType()
    {
        Tetramino.TetraminoType[] types = (Tetramino.TetraminoType[])System.Enum.GetValues(typeof(Tetramino.TetraminoType));
        int randomIndex = rnd.Next(7);
        return types[randomIndex];
    }
    public static Color Color(Tetramino.TetraminoType tetraminoType)
    {
        Color color = UnityEngine.Color.white;
        TetraminoColors tetraminoColors = ScriptableObjectsHolder.TetraminoColors;
        switch (tetraminoType)
        {
            case Tetramino.TetraminoType.I:
                color = 
                    //new Color(0f, 0.7686275f, 0.8666667f)
                    tetraminoColors.I
                    ;
                break;
            case Tetramino.TetraminoType.J:
                color = 
                    //new Color(0f, 0.4509804f, 0.8431373f)
                    tetraminoColors.J
                    ;

                break;
            case Tetramino.TetraminoType.L:
                color = 
                    //new Color(0.8078432f, 0.5333334f, 0f)
                    tetraminoColors.L
                    ;

                break;
            case Tetramino.TetraminoType.O:
                color =
                //new Color(0.8627452f, 0.7921569f, 0f)
                tetraminoColors.O
                ;

                break;
            case Tetramino.TetraminoType.S:
                color =
                    //new Color(0f, 0.8392158f, 0.227451f)
                    tetraminoColors.S
                    ;

                break;
            case Tetramino.TetraminoType.T:
                color =
                    //new Color(0.7019608f, 0f, 0.8470589f)
                    tetraminoColors.T
                    ;

                break;
            case Tetramino.TetraminoType.Z:
                color =
                    //new Color(0.8274511f, 0f, 0f)
                    tetraminoColors.Z
                    ;

                break;
            default:
                Debug.LogError("Unsupported tetramino type: " + tetraminoType);
                break;
        }
        return color;
    }
}
