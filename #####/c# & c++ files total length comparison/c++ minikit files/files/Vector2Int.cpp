#include "Vector2Int.h"
#include <iostream>
Vector2Int::Vector2Int()
{
	//std::cout << "Vector2Int::Vector2Int()" << std::endl;
	x = 0;
	y = 0;
}

Vector2Int::Vector2Int(int x, int y) {
	//std::cout << "Vector2Int::Vector2Int(int x, int y)" << std::endl;
	this->x = x;
	this->y = y;
}

Vector2Int::operator std::string() {
	std::string x_str = std::to_string(x);
	std::string y_str = std::to_string(y);
	return "[" + x_str + ", " + y_str + "]";
}

Vector2Int::operator Vector2() {
	return Vector2(x, y);
}
Vector2Int Vector2Int::operator+(Vector2Int const& obj) 
{
	return Vector2Int(x + obj.x, y + obj.y);
}

Vector2Int Vector2Int::operator-(Vector2Int const& obj)
{
	return Vector2Int(x - obj.x, y - obj.y);
}
Vector2 Vector2Int::operator*(int multiplier) {
	return Vector2(x*multiplier, y*multiplier);
}