#pragma once
#include "Tetromino.h"
class DrawProjection
{
public:
	Tetromino* tetrominoToTrack;
	Tetromino* projection;
	void Project();
private:

	Vector2Int StopPosition(Tetromino& tetromino);
};

