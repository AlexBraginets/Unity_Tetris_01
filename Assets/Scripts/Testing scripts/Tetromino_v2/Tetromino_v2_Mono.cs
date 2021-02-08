using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;

public class Tetromino_v2_Mono : MonoBehaviour
{
    [SerializeField] TetraminoMono tetraminoMono;
    //[SerializeField] Tetramino.TetraminoType tetrominoType;
    [SerializeField] Transform tetrominoCellPrefab;
    [SerializeField] Transform rotationTransform;
    [SerializeField] Transform[] cellTransforms;
    private void Start()
    {
        Build(tetraminoMono.tetramino.type);
    }
    private void Build(Tetramino.TetraminoType tetrominoType)
    {
        Tetramino tetromino = new Tetramino(tetrominoType);
        Vector3 rotationPoint = tetromino.rotationPoint;
        Vector2Int[] poses = tetromino.Poses;
        

        Vector3 w_tetrominoPosition = transform.position;
        Vector3 l_rotationTransformPosition = rotationPoint;
        Vector3[] l_cellTransformPositions = CellPosesRelative2RotationPoint(poses, rotationPoint);

        GameObject rotation_gameObject = new GameObject("Rotation transform");
        rotationTransform = AssignParentAndLocalPosition(rotation_gameObject, transform, l_rotationTransformPosition);

        List<Transform> cellTransformsList = new List<Transform>();
        int cellIndex = 0;
        foreach(Vector3 pos in l_cellTransformPositions)
        {
            GameObject cell_go = Instantiate(tetrominoCellPrefab).gameObject;
            cell_go.name = $"Cell index: {cellIndex++}";
            cellTransformsList.Add
                (
                AssignParentAndLocalPosition(cell_go, rotationTransform, pos)
                );
        }

        cellTransforms = cellTransformsList.ToArray();
    }
    private Vector3[] CellPosesRelative2RotationPoint(Vector2Int[] poses, Vector3 rotationPoint)
    {
        int length = poses.Length;
        Vector3[] result = new Vector3[length];
        for(int i = 0; i < length; i++)
        {
            result[i] = VectorUtil.V2Int_V3( poses[i]) - rotationPoint;
        }
        return result;
    }
    private Transform AssignParentAndLocalPosition(GameObject go, Transform parent, Vector3 localPosition)
    {
        go.transform.parent = parent;
        go.transform.localPosition = localPosition;
        return go.transform;
    }
    [ContextMenu("rotate both(tetromino<>this & tetromino mono) clockwise")]
    public void RotateClockwise()
    {
        tetraminoMono.RotateClockwise();
        rotationTransform.Rotate(Vector3.forward, -90f);
    }
}
