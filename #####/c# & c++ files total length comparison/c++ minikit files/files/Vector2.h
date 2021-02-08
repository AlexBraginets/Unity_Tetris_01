#pragma once
#include<string>
#include "Vector2Int.h"

class Vector2Int;

class Vector2
{
public:
	float x;
	float y;
	Vector2(float x, float y);
	Vector2();
	operator std::string();
	operator Vector2Int();
	Vector2 operator + (Vector2 const& obj);
	Vector2 operator - (Vector2 const& obj);
	Vector2 operator * (float multiplier);
};

