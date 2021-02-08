using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    // instance of Grid for reference purposes
    public static Grid Ins;
    // size of the Tetris play grid
    public static Vector2Int gridSize = new Vector2Int(10, 25);
    // size of a single tetramino cell
    // WARNING: could cause unexpected behaviour due to missing multiplying by offset where necessary
    public static float offset = 1f;
    // cache reference
    static Grid()
    {
        Ins = new Grid();
    }
    // 2D array for storing what blocks on the grid are occupied & by what
    // also contains walls and floor information to created initial borders
    public IBlock[,] blocks;
    // constructor to initialise blocks array with borders
    public Grid()
    {
        int xCount = gridSize.x + 2;
        int yCount = gridSize.y + 1;
        blocks = new IBlock[xCount, yCount];
        FillBorders();
    }
    // fills borders into blocks array
    private void FillBorders()
    {
        FillLeftWall();
        FillRightWall();
        FillFloor();
    }
    // fills right wall into blocks array
    private void FillLeftWall()
    {
        int wallHeight = blocks.GetLength(1) - 1;
        int xPos = 0;
        for (int y = 1; y <= wallHeight; y++)
        {
            blocks[xPos, y] = new WallBlock(BlockType.LeftWall);
        }
    }
    // fills left wall into blocks array
    private void FillRightWall()
    {
        int wallHeight = blocks.GetLength(1) - 1;
        int xPos = blocks.GetLength(0) - 1;
        for (int y = 1; y <= wallHeight; y++)
        {
            blocks[xPos, y] = new WallBlock(BlockType.RightWall);
        }
    }
    // fills floor into blocks array
    private void FillFloor()
    {
        int floorWidth = blocks.GetLength(0);
        int yPos = 0;
        for (int x = 0; x < floorWidth; x++)
        {
            blocks[x, yPos] = new WallBlock(BlockType.Floor);
        }
    }
    // checks whether there would be a collison of the tetramino with the grid if the tetramino
    // was moved by offset and rotated according to rotation type
    public static bool Collision
        (Grid grid, Tetramino tetramino, Vector2Int offset, Tetramino.RotationType rotation)
    {
        bool collision = false;
        Vector2Int[] tryPoses = tetramino.GetAbsPoses(offset, rotation);
        foreach(Vector2Int pos in tryPoses)
        {
            if (0 <= pos.x && pos.x < grid.blocks.GetLength(0)
                && 0 <= pos.y && pos.y < grid.blocks.GetLength(1))
            {
                collision = grid.blocks[pos.x, pos.y] != null;
            }
            else
            {
                collision = true;
            }
            if (collision)
                break;
        }
        return collision;
    }
    // makes cells occupied by tetramino impassable
    public void FreezeTetraminoArea(TetraminoMono tetraminoMono)
    {
        //foreach (Vector2Int pos in )
        //{
        //    blocks[pos.x, pos.y] = new WallBlock(BlockType.Unspecified);
        //}
        Tetramino tetramino = tetraminoMono.tetramino;
        Vector2Int[] absPoses = tetramino.AbsPoses;
        LoopUtil.LoopAction((i) =>
        {
            blocks[absPoses[i].x, absPoses[i].y] = 
            new WallBlock(BlockType.Unspecified, tetraminoMono.GetChildGameObject(i));
        }
        , absPoses.Length);
        DisplayBlocks.UpdateBlocks();
    }

    // coordinates where tetramino stop after moving in projection direction
    // tetramino stops because of the grid's stopping blocks(saved in grid.blocks 2D array)
    public static Vector2Int StopPosition(Grid grid, Tetramino tetramino, Vector2Int projectionDir)
    {
        int offsetCount = 0;
        while (!Grid.Collision
            (grid, tetramino, projectionDir * offsetCount, Tetramino.RotationType.None))
        {
            offsetCount++;
            if (offsetCount > 100)
            {
                Debug.LogError("Collision doesn't work properly apparentlly.");
                break;
            }
        }
        if(offsetCount == 0)
        {
            Debug.LogError("?");
            Debug.Log(projectionDir);
            Debug.Break();
        }
        offsetCount--;
        Vector2Int stopPosition = tetramino.centerPos
           + projectionDir * offsetCount;
        return stopPosition;

    }

    public static bool FullRow(int row)
    {
        bool full = true;
        int colCount = Grid.gridSize.x;
        Grid grid = Grid.Ins;
        IBlock[,] blocks = grid.blocks;
        for(int col = 1; col <= colCount; col++)
        {
            if(blocks[col, row] == null)
            {
                full = false;
                break;
            }
        }
        return full;
    }
    public static void EmptyAndDestroyBricksInRow(int rowIndex)
    {
        Grid grid = Grid.Ins;
        IBlock[,] blocks = grid.blocks;
        Vector2Int gridSize = Grid.gridSize;
        int colCount = gridSize.x;
        for (int colIndex = 1; colIndex <= colCount; colIndex++)
        {
            IBlock block = blocks[colIndex, rowIndex];
            if (block != null)
            {
                if (block.gameObject == null)
                {
                    Debug.LogError("Block contains null gameobject!");
                }
                GameObject.Destroy(block.gameObject);
                blocks[colIndex, rowIndex] = null;
            }

        }
    }
    public static void DropBricksInRow(int rowIndex, int dropCount)
    {

        Grid grid = Grid.Ins;
        IBlock[,] blocks = grid.blocks;
        Vector2Int gridSize = Grid.gridSize;
        int colCount = gridSize.x;
        for (int colIndex = 1; colIndex <= colCount; colIndex++)
        {
            IBlock block = blocks[colIndex, rowIndex];
            if (block != null)
            {
                if (block.gameObject == null)
                {
                    Debug.LogError("Block contains null gameobject!");
                }
                block.gameObject.transform.Translate(Vector3.down*dropCount);
                blocks[colIndex, rowIndex] = null;
                blocks[colIndex, rowIndex - dropCount] = block;
            }

        }
    }
}
