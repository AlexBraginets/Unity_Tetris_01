using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;

public class HorizontalInput : MonoBehaviour
{
    private bool pressedDown = false;
    private bool released = true;
    private float timePassedSinceKeyPressed = 0f;
    public float maxClamp = 0.5f;
    public float minClamp = 0.2f;
    public float moveUpdate = 0.2f;
    private float moveUpdateTimer = 0f;
    public Vector2 multiplier = new Vector2(6f, 0f);
    public KeyCode key;
    private float Speed(KeyCode key, float deltaTime)
    {
        float speed = 0f;
        if (Input.GetKeyDown(key))
        {
            pressedDown = true;
            released = false;
            timePassedSinceKeyPressed = 0f;
            moveUpdateTimer = moveUpdate;
        }
        else if (Input.GetKeyDown(key))
        {
            pressedDown = false;
            released = true;
            timePassedSinceKeyPressed = 0f;
            moveUpdateTimer = moveUpdate;
        }
        else if (Input.GetKey(key))
        {
            timePassedSinceKeyPressed += deltaTime;
            speed = Mathf.Clamp(timePassedSinceKeyPressed, minClamp, maxClamp);
        }
        return speed;
    }
    private Vector2 Movement(KeyCode key, float deltaTime)
    {
        float speed = Speed(key, deltaTime);
        
        Vector2 movement = Vector2.zero;
        moveUpdateTimer += deltaTime;
        if(moveUpdateTimer >= moveUpdate)
        {
            moveUpdateTimer = 0f;
            movement = VectorUtil.V2_V2Int(multiplier*speed);
        }
        if (key == KeyCode.D)
        {
            Debug.Log(speed + " : " + multiplier + " : " + movement);
        }
        return movement;
    }
    public Vector2 Movement(float deltaTime)
    {
        var movement = Movement(key, deltaTime);
        if(key == KeyCode.D) {
            //Debug.Log(movement + " : " + deltaTime);
        }
        return movement;
    }
}
