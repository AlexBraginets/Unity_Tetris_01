#pragma once
#include "Vector2.h"
#include "Block.h"
class Grid
{
public:
	Grid* ins;
	inline static Vector2Int gridSize = Vector2Int(10, 25);
	inline static float offset = 100.0f;
	Block* blocks[10 + 2][25 + 1];
	const static int blocksXCount = 12; // 10 + 2
	const static int blocksYCount = 26; // 25 + 1
	Grid();
	void FillBorders();
	void FillRightWall();
	void FillLeftWall();
	void FillFloor();
	bool Collision(Tetromino& tetromino, Vector2Int offset, Tetromino::RotationType rotation);
	Vector2Int StopPosition(Tetromino& tetromino, Vector2Int projectionDir);
};

