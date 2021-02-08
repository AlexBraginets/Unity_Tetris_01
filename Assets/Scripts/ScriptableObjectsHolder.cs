using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptableObjectsHolder
{
    private static string tetraminoColorsPath = "ScriptableObject Data instances/TetraminoColors";
    public static TetraminoColors TetraminoColors
    {
        get
        {
            if(tetraminoColors == null)
            {
                tetraminoColors = (TetraminoColors)Resources.Load<ScriptableObject>(tetraminoColorsPath);
            }
            return tetraminoColors;
        }
    }
    private static TetraminoColors tetraminoColors;
}
