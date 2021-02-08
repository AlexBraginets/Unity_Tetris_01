#pragma once
#include "Tetromino.h"

class Block
{
public:
	Tetromino* tetromino;
	int childIndex;
	enum class BlockType 
	{
		LeftWall, RightWall, Floor, Tetromino
	};
	BlockType type;
	Block(BlockType type);
};

