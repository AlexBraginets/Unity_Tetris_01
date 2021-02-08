#include "TetrominoUtil.h"
#include <ctime>
#include <cstdlib>

Tetromino::TetrominoType TetrominoUtil::RandomType() {
    srand((unsigned)time(0));
    int randomNumber = rand() % 7;
    return (Tetromino::TetrominoType)randomNumber;
}

::MiniKit::Graphics::Color TetrominoUtil::Color(Tetromino::TetrominoType tetraminoType) {
    ::MiniKit::Graphics::Color color{ 1.0f, 1.0f, 1.0f, 1.0f };
    switch (tetraminoType)
    {
    case Tetromino::TetrominoType::I:
        color =
            ::MiniKit::Graphics::Color{ 0.0f, 0.7686275f, 0.8666667f, 1.0f };
        break;
    case Tetromino::TetrominoType::J:
        color =
            ::MiniKit::Graphics::Color{ 0.0f, 0.4509804f, 0.8431373f, 1.0f };
        break;
    case Tetromino::TetrominoType::L:
        color =
            ::MiniKit::Graphics::Color{ 0.8078432f, 0.5333334f, 0.0f, 1.0f };
        break;
    case Tetromino::TetrominoType::O:
        color =
            ::MiniKit::Graphics::Color{ 0.8627452f, 0.7921569f, 0.0f, 1.0f };
        break;
    case Tetromino::TetrominoType::S:
        color =
            ::MiniKit::Graphics::Color{ 0.0f, 0.8392158f, 0.227451f, 1.0f };
        break;
    case Tetromino::TetrominoType::T:
        color =
            ::MiniKit::Graphics::Color{ 0.7019608f, 0.0f, 0.8470589f, 1.0f };
        break;
    case Tetromino::TetrominoType::Z:
        color =
            ::MiniKit::Graphics::Color{ 0.8274511f, 0.0f, 0.0f, 1.0f };
        break;
    default:
        throw "colors error";
        break;
    }
    return color;
}
