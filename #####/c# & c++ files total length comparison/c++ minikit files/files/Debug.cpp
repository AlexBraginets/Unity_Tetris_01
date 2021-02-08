#include "Debug.h"
#include <iostream>
#include <string>

void Debug::Log(std::string message) {
	std::cout << "Debug.Log: " << message << std::endl;
}

void Debug::Log() {
	std::cout << std::endl;
}