using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonstrateAllTetraminoTypes : MonoBehaviour
{
    public float offset = 5;
    void Start()
    {
        int tetraminoTypeIndex = 0;
        foreach (Tetramino.TetraminoType tetraminoType in System.Enum.GetValues(typeof(Tetramino.TetraminoType)))
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.right*offset*tetraminoTypeIndex;
            TetraminoMono tetraminoMono = go.AddComponent<TetraminoMono>();
            tetraminoMono.type = tetraminoType;
            tetraminoTypeIndex++;
        }
    }
    
}
