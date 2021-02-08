#include "Tetromino.h"
#include<string>
#include<vector>
#include "VectorUtil.h"
Tetromino::Tetromino(TetrominoType tetrominoType)
{
    this->tetrominoType = tetrominoType;
    switch (tetrominoType)
    {
    case TetrominoType::I:
    {
        int poses1[4][2] = { {0, 0},{1, 0},{2, 0},{3, 0} };
        Poses = GetPoses(poses1);
        rotationPoint = Vector2(1.5, -0.5f);
    }
    break;
    case TetrominoType::J:
    {
        int poses2[4][2] = { {0, 0}, {1 , 0}, {1, 1}, {1, 2} };
        Poses = GetPoses(poses2);
        rotationPoint = Vector2(1.f, 1.f);
    }
    break;
    case TetrominoType::L:
    {
        int poses3[4][2] = { {0, 0}, {1, 0}, {0, 1}, {0, 2} };
        Poses = GetPoses(poses3);
        rotationPoint = Vector2(0.f, 1.f);
    }
    break;
    case TetrominoType::O:
    {
        int poses4[4][2] = { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } };
        Poses = GetPoses(poses4);
        rotationPoint = Vector2(1.f / 2.f, 1.f / 2.f);
    }
    break;
    case TetrominoType::S:
    {
        int poses5[4][2] = { {0, 0}, {1 , 0}, {1, 1}, {2, 1} };
        Poses = GetPoses(poses5);
        rotationPoint = Vector2(1.f, 0.f);
    }
    break;
    case TetrominoType::T:
    {
        int poses6[4][2] = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } };
        Poses = GetPoses(poses6);
        rotationPoint = Vector2(1.f, 1.f);
    }
    break;
    case TetrominoType::Z:
    {
        int poses7[4][2] = { {0, 1}, {1, 1}, {1, 0}, {2, 0} };
        Poses = GetPoses(poses7);
        rotationPoint = Vector2(1.f, 0.f);
    }
    break;
    default:
        throw 5;
        break;
    }
}


std::vector<Vector2Int> Tetromino::GetPoses(int(&doubleArray)[4][2])
{
    auto vectorPoses = std::vector<Vector2Int>();
    vectorPoses.resize(4);

    for (int posIndex = 0; posIndex < 4; posIndex++)
    {
        int x = doubleArray[posIndex][0];
        int y = doubleArray[posIndex][1];
        vectorPoses[posIndex] = Vector2Int(x, y);
    }
    return vectorPoses;
}

Tetromino::operator std::string() {
    std::string message;
    std::string v0 = Poses[0];
    std::string v1 = Poses[1]; 
    std::string v2 = Poses[2];
    std::string v3 = Poses[3];
    message = "{" + v0 + ", " +  v1 + ", " + v2 + ", " + v3 + "}";
    return message;
}

std::vector<Vector2Int> Tetromino::GetAbsPoses(Vector2Int offset, Tetromino::RotationType rotation)
{
    std::vector<Vector2Int> rotated = Tetromino::RotateArray(Poses, rotationPoint, rotation);
    for (int  i = 0; i < rotated.size(); i++)
    {
        rotated[i] = rotated[i] + offset + centerPos;
    }
    return rotated;
}

std::vector<Vector2Int> Tetromino::GetAbsPoses()
{
    return GetAbsPoses(Vector2Int(), RotationType::None);
}

void Tetromino::RotateClockwise() {
    Poses = RotateArray(Poses, rotationPoint, RotationType::Clockwise);
}


std::vector<Vector2Int> Tetromino::RotateArray(const std::vector<Vector2Int>& poses, const Vector2& rotationPoint, const RotationType& rotationType)
{
    int length = poses.size();
    std::vector<Vector2Int> rotatedPoses = std::vector<Vector2Int>();
    rotatedPoses.resize(length);
    for (int i = 0; i < length; i++)
    {
        Vector2Int pos = poses[i];
        Vector2Int rotatedPos;
        switch (rotationType)
        {
        case RotationType::None:
            rotatedPos = pos;
            break;
        case RotationType::Clockwise:
            rotatedPos = VectorUtil::RotateClockwise(pos, rotationPoint);
            break;
        case RotationType::CounterClockwise:
            rotatedPos = VectorUtil::RotateCounterClockwise(pos, rotationPoint);
            break;
        default:
            throw 5;
            break;
        }
        rotatedPoses[i] = rotatedPos;
    }
    return rotatedPoses;
}
void Tetromino::TranslateCenterPosition(Vector2Int offset)
{
    centerPos = centerPos + offset;
}

