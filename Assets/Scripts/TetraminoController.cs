using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;

public class TetraminoController : MonoBehaviour
{
    public TetraminoMono toControl;
    public DrawProjection projectionDrawer;
    public static TetraminoController Ins;
    private TetraminoMono projection;
    private void Start()
    {
        TetraminoController.Ins = this;
        UpdateProjection();
    }
    public void UpdateProjection()
    {
        projection = projectionDrawer.SetUpProjection(toControl);
    }
    private void Update()
    {
        TetraminoController.ProcessMovementAndRotation(toControl, projection);
    }
    private static void ProcessMovementAndRotation(TetraminoMono tetraminoMono, TetraminoMono projection)
    {
        ProcessHorizontal(tetraminoMono);
        ProcessRotation(tetraminoMono, projection);
    }
    private static void ProcessHorizontal(TetraminoMono tetraminoMono)
    {
        //ProcessHorizontal(Grid.Ins, tetraminoMono, ProcessHorizontalInput.Movement);
        Vector2Int horMovement;
        bool processHorizontal = ProcessHorizontalInput(out horMovement);
        if (processHorizontal)
        {
            ProcessHorizontal(Grid.Ins, tetraminoMono, horMovement);
        }
    }
    private static bool ProcessHorizontalInput(out Vector2Int movement)
    {
        bool processHorizontal;
        if (Input.GetKeyDown(KeyCode.A))
        {
            processHorizontal = true;
            movement = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movement = Vector2Int.right;
            processHorizontal = true;
        }
        else
        {
            movement = Vector2Int.zero;
            processHorizontal = false;
        }
        return processHorizontal;
    }
    private static void ProcessHorizontal(Grid grid, TetraminoMono tetraminoMono, Vector2Int offset)
    {
        if (offset.sqrMagnitude == 0)
            return;
        Tetramino tetraminoData = tetraminoMono.tetramino;
        
        //Vector2Int projectionDir = VectorUtil.V2_V2Int(((Vector2)offset).normalized);
        Vector2Int projectionDir = offset;
        Vector2Int stopPosition = Grid.StopPosition(grid, tetraminoData, projectionDir);
        Vector2Int stopDeltaPosition = stopPosition - tetraminoData.centerPos;
        int stopDeltaPositionSqrManitude = stopDeltaPosition.sqrMagnitude;
        int offsetPositionSqrManitude = offset.sqrMagnitude;
        if(stopDeltaPositionSqrManitude < offsetPositionSqrManitude)
        {
            tetraminoMono.SetCenterPosition(stopPosition);
            Debug.Log("stopDeltaPositionSqrManitude < offsetPositionSqrManitude");
            // tetramino can't move because of grid blocks being occupied
        }
        else
        {
            tetraminoMono.TranslateCenterPosition(offset);
        }
    }
    private static void ProcessRotation(TetraminoMono tetraminoMono, TetraminoMono projection)
    {
        Tetramino tetraminoData = tetraminoMono.tetramino;
        if (Input.GetKeyDown(KeyCode.W))
        {
            bool collision =
                Grid.Collision(Grid.Ins, tetraminoData, Vector2Int.zero, Tetramino.RotationType.Clockwise);
            if (!collision)
            {
                tetraminoMono.RotateClockwise();
                projection.RotateClockwise();
            }
        }
    }
}
