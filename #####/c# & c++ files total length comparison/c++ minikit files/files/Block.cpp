#include "Block.h"

Block::Block(BlockType type) 
{
	this->type = type;
	tetromino = NULL;
	childIndex = -1;
}