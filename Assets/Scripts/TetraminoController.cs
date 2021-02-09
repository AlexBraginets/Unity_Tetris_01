using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorUtilLibrary;

public class TetraminoController : MonoBehaviour
{
    public static TetraminoController Ins;
    public TetraminoMono toControl;
    public DrawProjection projectionDrawer;
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
        ProcessMovementAndRotation(toControl, projection);
    }
    private static void ProcessMovementAndRotation(TetraminoMono tetraminoMono, TetraminoMono projection)
    {
        ProcessHorizontal(tetraminoMono);
        ProcessRotation(tetraminoMono, projection);
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
    private static void ProcessHorizontal(Grid grid, TetraminoMono tetraminoMono, Vector2Int offset)
    {
        if (offset.sqrMagnitude == 0)
            return;
        Tetramino tetraminoData = tetraminoMono.tetramino;
        //bool collision =
        //    Grid.Collision(grid, tetraminoData, offset, Tetramino.RotationType.None);
        //if (!collision)
        //{
        //    tetraminoMono.TranslateCenterPosition(offset);
        //}
        Vector2Int projectionDir = VectorUtil.V2_V2Int(((Vector2)offset).normalized);
        Vector2Int stopPosition = Grid.StopPosition(Grid.Ins, tetraminoData, projectionDir);
        Vector2Int stopDeltaPosition = stopPosition - tetraminoData.centerPos;
        int stopDeltaPositionSqrManitude = stopDeltaPosition.sqrMagnitude;
        int offsetPositionSqrManitude = offset.sqrMagnitude;
        if((stopDeltaPositionSqrManitude < offsetPositionSqrManitude))
        {
            tetraminoMono.SetCenterPosition(stopPosition);
            Debug.Log("stopDeltaPositionSqrManitude < offsetPositionSqrManitude");// INTERESTING STUFF
        }
        else
        {
            tetraminoMono.TranslateCenterPosition(offset);
        }
    }
}
