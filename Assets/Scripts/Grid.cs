using UnityEngine;
using CommonNonUnityUtils;

public class Grid
{
    // instance of Grid for reference purposes
    public static Grid Ins;
    // size of the Tetris play grid
    public static Vector2Int gridSize = new Vector2Int(10, 25);
    // hide height count at the top
    private const int HIDE_HIGHT_COUNT = 5;
    public static bool HideBlock(int row)
    {
        int wallHeight = gridSize.y;
        return wallHeight - row <= HIDE_HIGHT_COUNT;
    }
    // size of a single tetramino cell
    // WARNING: could cause unexpected behaviour due to the inconsistency of the use of the variable
    public static float offset = 1f;
    // cache reference
    static Grid()
    {
        Ins = new Grid();
    }
    // 2D array for storing what blocks on the grid are occupied & by what
    // also contains walls and floor information for created initial borders
    public IBlock[,] blocks;
    public IBlock this[Vector2Int pos]
    {
        get
        {
            return blocks[pos.x, pos.y];
        }
        set
        {
            blocks[pos.x, pos.y] = value;
        }
    }
    // constructor: initialises blocks array with borders
    private Grid()
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
    // fills left wall into blocks array
    private void FillLeftWall()
    {
        int wallHeight = blocks.GetLength(1) - 1;
        int xPos = 0;
        for (int y = 1; y <= wallHeight; y++)
        {
            blocks[xPos, y] = new WallBlock(BlockType.LeftWall);
        }
    }
    // fills right wall into blocks array
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
        int floorWidth = blocks.GetLength(0) - 1;
        int yPos = 0;
        for (int x = 0; x <= floorWidth; x++)
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
        foreach (Vector2Int pos in tryPoses)
        {
            if (0 <= pos.x && pos.x < grid.blocks.GetLength(0)
                && 0 <= pos.y && pos.y < grid.blocks.GetLength(1))
            {
                collision = grid[pos] != null;
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
        Tetramino tetramino = tetraminoMono.tetramino;
        Vector2Int[] absPoses = tetramino.AbsPoses;
        LoopUtil.LoopAction((i) =>
        {
            this[absPoses[i]] =
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
        if (offsetCount == 0)
        {
            Debug.Log("Tetramino already is in position where it can't be.");
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
        for (int col = 1; col <= colCount; col++)
        {
            if (blocks[col, row] == null)
            {
                full = false;
                break;
            }
        }
        return full;
    }
    public static void EmptyAndDestroyBricksInRow(int rowIndex)
    {
        Grid.Ins._EmptyAndDestroyBricksInRow(rowIndex);
    }
    private void _EmptyAndDestroyBricksInRow(int rowIndex)
    {
        System.Action<int> action = colIndex =>
        {
            IBlock block = blocks[colIndex, rowIndex];
            if (block != null)
            {
                GameObject blockGameObject = block.gameObject;
                if (blockGameObject == null)
                {
                    Debug.LogError("Block contains null gameobject!");
                }
                GameObject.Destroy(blockGameObject);
                blocks[colIndex, rowIndex] = null;
            }
        };
        ActionInRow(action);
    }
    public static void DropBricksInRow(int rowIndex, int dropCount)
    {
        Grid.Ins._DropBricksInRow(rowIndex, dropCount);
    }
    private void _DropBricksInRow(int rowIndex, int dropCount)
    {
        System.Action<int> action = colIndex =>
        {
            IBlock block = blocks[colIndex, rowIndex];
            if (block != null)
            {
                GameObject blockGameObject = block.gameObject;
                if (blockGameObject == null)
                {
                    Debug.LogError("Block contains null gameobject!");
                }
                Transform blockTransform = blockGameObject.transform;
                blockTransform.Translate(dropCount * Vector3.down);
                blocks[colIndex, rowIndex] = null;
                blocks[colIndex, rowIndex - dropCount] = block;
            }
        };
        ActionInRow(action);
    }
    private static void ActionInRow(System.Action<int> action)
    {
        int colCount = gridSize.x;
        // TO FIX: can redo via LoopUtil.Action but show do something with iterator 'cause in LoopUtil.Action
        // it iterator is 0 at the start, but I need 1 at the start
        for (int colIndex = 1; colIndex <= colCount; colIndex++)
        {
            action.Invoke(colIndex);
        }
    }
}
