using UnityEngine;

public class DemonstrateAllTetraminoTypes : MonoBehaviour
{
    public float offset = 5;
    void Start()
    {
        int tetraminoTypeIndex = 0;
        foreach (Tetramino.TetraminoType tetraminoType in EnumUtil.GetValues<Tetramino.TetraminoType>())
        {
            Vector3 localPosition = Vector3.right * offset * tetraminoTypeIndex;
            GameObject go = InstantiateChildEmptyGameObject(localPosition);
            TetraminoMono tetraminoMono = go.AddComponent<TetraminoMono>();
            tetraminoMono.type = tetraminoType;
            tetraminoTypeIndex++;
        }
    }
    private GameObject InstantiateChildEmptyGameObject(Vector3 localPosition)
    {
        GameObject go = new GameObject();
        go.transform.parent = transform;
        go.transform.localPosition = localPosition;
        return go;
    }
    
}
