using UnityEngine;
using CommonNonUnityUtils;
public class DisplayBlocks : MonoBehaviour
{
    public GameObject prefab;
    private GameObject[,] blockGameObjects;
    private IBlock[,] blocks = Grid.Ins.blocks;
    public static DisplayBlocks Ins;
    private void Awake()
    {
        Ins = this;
    }
    void Start()
    {
        var xCount = blocks.GetLength(0);
        var yCount = blocks.GetLength(1);
        blockGameObjects = new GameObject[xCount, yCount];
        LoopUtil.LoopAction((x, y) =>
        {
            blockGameObjects[x, y] = Instantiate
            (prefab, new Vector2(x, y), Quaternion.identity, transform);
        }
        , xCount, yCount);
        _UpdateBlocks();
    }

    
    public static void UpdateBlocks()
    {
        if (DisplayBlocks.Ins != null)
            DisplayBlocks.Ins._UpdateBlocks();
        
    }
    private void _UpdateBlocks()
    {
        var xCount = blocks.GetLength(0);
        var yCount = blocks.GetLength(1);
        LoopUtil.LoopAction((x, y) =>
        {
            blockGameObjects[x, y].SetActive(blocks[x, y] != null);
        }
        , xCount, yCount);
    }

}
