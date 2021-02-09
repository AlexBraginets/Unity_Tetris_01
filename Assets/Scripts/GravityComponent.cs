using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(100)]
public class GravityComponent : MonoBehaviour
{
    public float fallTime = 1f;
    public DrawProjection projection;
    [SerializeField] private float timer;
    private TetraminoMono tetraminoMono;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        CacheTetraminoComponent();
    }
    private void CacheTetraminoComponent()
    {
        tetraminoMono = GetComponent<TetraminoMono>();
        if(tetraminoMono == null)
        {
            Debug.LogError("No tetramino mono!");
        }
    }
    // Update is called once per frame
    void Update()
    {

        bool update = UpdateTimer(Time.deltaTime);
        bool instantMove = Input.GetKeyDown(KeyCode.Space);
        if (update || instantMove)
        {
            Grid grid = Grid.Ins;
            Tetramino tetramino = tetraminoMono.tetramino;
            Vector2Int offset = Vector2Int.down;
            bool spawnNewTetramino = false; 
            if (instantMove)
            {
                //Vector2Int projectionPos = projection.projection.GetComponent<TetraminoMono>().tetramino.centerPos;
                //Vector2Int tetraminoPos = tetramino.centerPos;
                //offset = projectionPos - tetraminoPos;
                spawnNewTetramino = true;
            }
            else
            {
                bool collision = Grid.Collision(grid, tetramino, Vector2Int.down, Tetramino.RotationType.None);
                if (collision)
                {
                    //Debug.Log("collision");
                    spawnNewTetramino = true;
                }
                else
                {
                    tetraminoMono.TranslateCenterPosition(offset); 
                }
            }
            if (spawnNewTetramino)
            {
                Tetramino tetraminoProjectionCopy = projection.DropProjection();  //TODO Check exact spawn time
                SpawnNewTetramino();
                SweepLine.Sweep(tetraminoProjectionCopy);
            }
        }
    }
    private void SpawnNewTetramino()
    {
        Vector2Int spawnPosition = new Vector2Int(1, 21);
        tetraminoMono.UploadNewTetraminoData(TetraminoUtil.RandomType() );
        //tetraminoMono.UploadNewTetraminoData(Tetramino.TetraminoType.O);

        tetraminoMono.SetCenterPosition(spawnPosition);
        tetraminoMono.RedoInit();
        TetraminoController.Ins.UpdateProjection();
    }
    private bool UpdateTimer(float deltaTime)
    {
        timer -= deltaTime;
        if(timer <= 0)
        {
            ResetTimer();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void ResetTimer()
    {
        timer = fallTime;
    }
}
