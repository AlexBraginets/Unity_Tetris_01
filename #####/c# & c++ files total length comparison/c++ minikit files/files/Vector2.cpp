#include "Vector2.h"

#include<iostream>
Vector2::operator std::string() {
	std::string x_str = std::to_string(x);
	std::string y_str = std::to_string(y);
	return "[" + x_str + ", " + y_str + "]";
}

Vector2::Vector2() 
{
	x = 0;
	y = 0;
}

Vector2::Vector2(float x, float y) {
	this->x = x;
	this->y = y;
}
Vector2::operator Vector2Int()
{
	return Vector2Int(std::round(x), std::round(y));
}

Vector2 Vector2::operator + (Vector2 const& obj) {
	return Vector2(x + obj.x, y + obj.y);
}

Vector2 Vector2::operator - (Vector2 const& obj) {
	return Vector2(x - obj.x, y - obj.y);
}

Vector2 Vector2::operator*(float multiplier)
{
	return Vector2(x * multiplier, y * multiplier);
}
