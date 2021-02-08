#include "VectorUtil.h"

Vector2Int VectorUtil::RotateClockwise(Vector2Int point, Vector2 rotationPoint) 
{
    Vector2 pointRaw = ((Vector2)point) - rotationPoint;
    float x = pointRaw.y;
    float y = -pointRaw.x;
    Vector2 newpos =  Vector2(x, y) + rotationPoint;
    return newpos;
}

Vector2Int VectorUtil::RotateCounterClockwise(Vector2Int point, Vector2 rotationPoint)
{Vector2 pointRaw = ((Vector2)point) - rotationPoint;
    float x = -pointRaw.y;
    float y = pointRaw.x;
    Vector2 newpos = Vector2(x, y) + rotationPoint;
    return newpos;
}