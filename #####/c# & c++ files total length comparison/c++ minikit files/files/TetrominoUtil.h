#pragma once

#include <MiniKit/MiniKit.hpp>
#include "../../../../../source/repos/TetrisEngine_v2/TetrisEngine_v2/Tetromino.h"

class TetrominoUtil
{
public:
	static ::MiniKit::Graphics::Color Color(Tetromino::TetrominoType tetraminoType);
	static Tetromino::TetrominoType RandomType();

	
};

