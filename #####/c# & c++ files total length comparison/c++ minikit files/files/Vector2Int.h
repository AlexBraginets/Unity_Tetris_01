#pragma once
#include <string>
#include "Vector2.h"

class Vector2;

class Vector2Int {
public:
	int x;
	int y;
	Vector2Int(int x, int y);
	Vector2Int();
	operator std::string();
	operator Vector2();
	Vector2Int operator + (Vector2Int const& obj);
	Vector2Int operator - (Vector2Int const& obj);
	Vector2 operator*(int multiplier);
};