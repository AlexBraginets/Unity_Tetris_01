#pragma once
#include "Vector2Int.h"

static class VectorUtil
{
public:
	static Vector2Int RotateClockwise(Vector2Int point, Vector2 rotationPoint);
	static Vector2Int RotateCounterClockwise(Vector2Int point, Vector2 rotationPoint);
};

