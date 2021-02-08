using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (blocks[x, y] == null)
                blockGameObjects[x, y].SetActive(false);
        }
        , xCount, yCount);
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
            if (blocks[x, y] == null)
                blockGameObjects[x, y].SetActive(false);
            else
                blockGameObjects[x, y].SetActive(true);
        }
        , xCount, yCount);
    }

}
