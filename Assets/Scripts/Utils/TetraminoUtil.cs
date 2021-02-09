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
                color =   tetraminoColors.I;
                break;
            case Tetramino.TetraminoType.J:
                color = tetraminoColors.J;
                break;
            case Tetramino.TetraminoType.L:
                color = tetraminoColors.L;
                break;
            case Tetramino.TetraminoType.O:
                color = tetraminoColors.O;
                break;
            case Tetramino.TetraminoType.S:
                color = tetraminoColors.S;
                break;
            case Tetramino.TetraminoType.T:
                color = tetraminoColors.T;
                break;
            case Tetramino.TetraminoType.Z:
                color = tetraminoColors.Z;
                break;
            default:
                Debug.LogError("Unsupported tetramino type: " + tetraminoType);
                break;
        }
        return color;
    }
}
