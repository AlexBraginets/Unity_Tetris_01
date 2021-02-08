#include "Grid.h"
#include <string>
#include <iostream>
Grid::Grid() 
{
    FillBorders();
}

void Grid::FillBorders()
{
    for (int x = 0; x < Grid::blocksXCount; x++)
    {
        for (int y = 1; y < Grid::blocksYCount; y++)
        {
            blocks[x][y] = NULL;

        }

    }
    FillLeftWall();
    FillRightWall();
    FillFloor();
}

void Grid::FillLeftWall()
{
    int wallHeight = Grid::blocksYCount - 1;
    int xPos = 0;
    for (int y = 1; y <= wallHeight; y++)
    {
        blocks[xPos][y] = new Block(Block::BlockType::LeftWall);

    }
}

void Grid::FillRightWall()
{
    int wallHeight = Grid::blocksYCount - 1;
    int xPos = Grid::blocksXCount - 1;
    for (int y = 1; y <= wallHeight; y++)
    {
        blocks[xPos][y] = new Block(Block::BlockType::RightWall);
    }
}

void Grid::FillFloor()
{
    int floorWidth = Grid::blocksXCount;
    int yPos = 0;
    for (int x = 0; x < floorWidth; x++)
    {
        blocks[x][yPos] = new Block(Block::BlockType::Floor);
    }
}

bool Grid::Collision(Tetromino& tetromino, Vector2Int offset, Tetromino::RotationType rotation)
{
    bool collision = false;
    auto tryPoses = tetromino.GetAbsPoses(offset, rotation);
    for(Vector2Int pos : tryPoses)
    {
        if (0 <= pos.x && pos.x < Grid::blocksXCount//grid.blocks.GetLength(0)
            && 0 <= pos.y && pos.y < Grid::blocksYCount)//grid.blocks.GetLength(1))
        {
            collision = blocks[pos.x][pos.y] != NULL;
        }
        else
        {
            collision = true;
            std::cout <<  "collision" << std::endl;
        }
        if (collision)
            break;
    }
    return collision;
}

Vector2Int Grid::StopPosition(Tetromino& tetromino, Vector2Int projectionDir)
{
    int offsetCount = 0;
    while (!Grid::Collision
    (tetromino, projectionDir * offsetCount, Tetromino::RotationType::None))
    {
        offsetCount++;
        if (offsetCount > 100)
        {
            throw 5;
            break;
        }
    }
    if (offsetCount == 0)
    {
        throw 5;
    }
    offsetCount--;
    Vector2Int stopPosition = tetromino.centerPos
        + projectionDir * offsetCount;
    return stopPosition;

}

