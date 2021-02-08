using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonNonUnityUtils;
// builds Tetris playing grid at start
public class GridMono : MonoBehaviour
{
    // builds Tetris playing grid at start
    private void Start()
    {
        BuildGrid();
    }
    // builds Tetris playing grid according to the data specified in Grid class
    private void BuildGrid()
    {
        Transform parent = transform;
        Color color = Color.white;
        float offset = Grid.offset;
        int xCount = Grid.gridSize.x;
        int yCount = Grid.gridSize.y;
        
        LoopUtil.LoopAction(
            (x, y)
            =>
            {
                GameObject square =
                SquareUtil.InstantiateAndSetUpSquare(parent, offset, new Vector2(x, y), color);
                if (yCount - y <= 5)
                    square.SetActive(false);
            },
            xCount, yCount);
    }
}
