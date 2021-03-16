using UnityEngine;
[DefaultExecutionOrder(101)]
public class DrawProjection : MonoBehaviour
{
    // direction in which projection should be done
    private Vector2Int projectionDirection = Vector2Int.down;
    // tetramino that should be projected
    private TetraminoMono tetraminoToTrack;
    private Tetramino.TetraminoType type => tetraminoToTrack.type;
    // gameobject that is the projection of the tetramino
    //[HideInInspector]     #new
    private GameObject projection;
    private TetraminoMono projectionTetraminoMono => projection.GetComponent<TetraminoMono>();
    private Tetramino projectionTetraminoData => projectionTetraminoMono.tetramino;
    
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
        this.tetraminoToTrack = tetraminoToProject;
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
        projectionCopy.Rotate(rotationCount);
        Grid.Ins.FreezeTetraminoArea(projectionCopy);
        return projectionCopy.tetramino;
    }
}
