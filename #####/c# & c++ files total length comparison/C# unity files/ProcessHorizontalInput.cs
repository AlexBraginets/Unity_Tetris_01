using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessHorizontalInput : MonoBehaviour
{
    public HorizontalInput aInput;
    public HorizontalInput dInput;
    private static Vector2 aMovement = Vector2Int.zero;
    private static Vector2 dMovement = Vector2Int.zero;
    private void Update()
    {
        aMovement = aInput.Movement(Time.deltaTime);
        dMovement = dInput.Movement(Time.deltaTime);
    }
    public static Vector2Int Movement => VectorUtil.V2_V2Int( aMovement + dMovement);

}
