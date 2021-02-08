
#include <iostream>
#include <string>
#include "Debug.h"
#include "Vector2Int.h"
#include "Tetromino.h"
#include <vector>
#include "VectorUtil.h"
#include "Grid.h"
void PrintInfo(Vector2Int& v1, Vector2Int& v2)
{
	Debug::Log(v1);
	Debug::Log(v2);
	Debug::Log();
}
void Vector2Int_Test() {
	Vector2Int v1;
	Vector2Int v2 = v1;
	PrintInfo(v1, v2);

	v1.x = 2;
	v2.y = 3;
	PrintInfo(v1, v2);
}
void Testing() {
	Vector2Int_Test();
}
int main()
{
	//Grid::ins = new Grid();
	Tetromino tetromino = Tetromino(Tetromino::TetrominoType::S);
	std::vector<Vector2Int> poses = tetromino.Poses;
	for (int i = 0; i < 5; i++) {
		std::string tetraminoString = tetromino;
		Debug::Log("Rotated clockwise count: " + std::to_string(i) + ", Positions: " + tetraminoString);
		for (int posIndex = 0; posIndex < 4; posIndex++)
		{
			poses[posIndex] = VectorUtil::RotateClockwise(poses[posIndex], tetromino.rotationPoint);
		}
		tetromino.Poses = poses;
	}
}


