using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    private void Start()
    {
       // Debug.Log("DrawProjection start");
    }
    private Tetramino projectionTetraminoData => projectionTetraminoMono.tetramino;
    private Tetramino.TetraminoType type => tetraminoToTrack.type;
    // tetramino that should be projected
    public TetraminoMono tetraminoToTrack;
    // gameobject that is the projection of the tetramino
    //[HideInInspector]     #new
    public GameObject projection;
    // direction in which projection should be done
    private Vector2Int projectionDirection = Vector2Int.down;
    private TetraminoMono projectionTetraminoMono
    {
        get
        {
            return projection.GetComponent<TetraminoMono>();
        }
    }
    // show projection in the scene
    public void Draw(TetraminoMono tetraminoToProject)
    {
        Vector2Int projectionCenterPosition = StopPosition(tetraminoToProject);
        projectionTetraminoMono.SetCenterPosition(projectionCenterPosition);
    }
    private Vector2Int StopPosition(TetraminoMono tetraminoMono)
    {
        return Grid.StopPosition(Grid.Ins, tetraminoMono.tetramino, projectionDirection);
    }
    // creates projection GameObject based on "tetramino to project"
    public TetraminoMono SetUpProjection(TetraminoMono tetraminoToProject)
    {
        Destroy(this.projection);
        this.projection = TetraminoMono.Instantiate(type, "DrawProjection.SetUpProjection");
        projectionTetraminoMono.SetOpacity(0.3f);
        this.projection.transform.parent = transform;
        return projectionTetraminoMono;
    }
    // update projection position each frame
    private void Update()
    {
        Draw(tetraminoToTrack);
    }
    // creates copy of projection GameObject at projection position,
    // but adjusts some things(like opacity). Also makes blocks where projection had been
    // stoppable for tetramino controlled by player
    public Tetramino DropProjection()
    {
        Vector2Int projectionCenterPos = projectionTetraminoData.centerPos;
        int rotationCount = projectionTetraminoData.rotationCount;


        TetraminoMono projectionCopy = TetraminoMono.Instantiate(type).GetComponent<TetraminoMono>();
        projectionCopy.transform.parent = transform;
        projectionCopy.SetOpacity(1f);
        projectionCopy.SetCenterPosition(projectionTetraminoData.centerPos);
        projectionCopy.tetramino.Rotate(projectionTetraminoData.rotationCount);
        projectionCopy.UpdatePosesAfterRotation();
        Grid.Ins.FreezeTetraminoArea(projectionCopy);
        return projectionCopy.tetramino;
        //Debug.Log("projection rotation count: " + projectionTetraminoData.rotationCount);
        //Debug.Log("tetramino to track rotation count: " + tetraminoToTrack.tetramino.rotationCount);
    }
}
