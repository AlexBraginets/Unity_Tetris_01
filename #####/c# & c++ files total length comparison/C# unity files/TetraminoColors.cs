using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TetraminoColors", menuName = "ScriptableObjects/TetraminoColors", order = 1)]
public class TetraminoColors : ScriptableObject
{
    [Header("Tetramino colors:")]
    public Color I;
    public Color O;
    public Color T;
    public Color J;
    public Color L;
    public Color S;
    public Color Z;
}
