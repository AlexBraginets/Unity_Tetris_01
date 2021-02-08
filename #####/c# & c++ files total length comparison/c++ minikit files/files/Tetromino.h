#pragma once
#include "Vector2Int.h"
#include <vector>
class Tetromino
{
public:
    enum class TetrominoType
    {
        I, O, T, J, L, S, Z
    };
    enum class RotationType
    {
        None, Clockwise, CounterClockwise
    };
    TetrominoType tetrominoType;
    Vector2Int centerPos;
    Vector2 rotationPoint;
    std::vector<Vector2Int> Poses;
    Tetromino(TetrominoType tetrominoType);
    
    std::vector<Vector2Int> GetPoses(int(&doubleArray)[4][2]);
    operator std::string();
    std::vector<Vector2Int> GetAbsPoses(Vector2Int offset, RotationType rotation);
    std::vector<Vector2Int> GetAbsPoses();
    void RotateClockwise();
    static std::vector<Vector2Int> RotateArray(const std::vector<Vector2Int>& poses, const Vector2& rotationPoint, const RotationType& rotationType);
    void TranslateCenterPosition(Vector2Int offset);
   
};

