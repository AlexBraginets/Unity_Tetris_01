using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TetraminoUtils;
using CommonNonUnityUtils;
using VectorUtilLibrary;
using TransformUtilLibrary;
public class TetraminoMono : MonoBehaviour
{
    [Header("Choose one of seven tetramino types")]
    public Tetramino.TetraminoType type;
    private float offset => Grid.offset;
    private Tetramino _tetramino = null;
    public Tetramino tetramino
    {
        get
        {
            if (_tetramino == null)
            {
                _tetramino = new Tetramino(type);
            }
            return _tetramino;
        }
    }
    public Color color
    {
        get
        {
            Color tetraminoColor = TetraminoUtil.Color(type);
            return ColorUtil.ColorWithOpacity(tetraminoColor, opacity);
        }
    }
    private bool inited = false;
    public void UploadNewTetraminoData(Tetramino.TetraminoType type)
    {
        this._tetramino = new Tetramino(type);
    }
    public GameObject GetChildGameObject(int index)
    {
        if (index < 0 || index >= 4)
        {
            Debug.Log("Cannot get child with index less than 0 or more then 3!");
        }
        return transform.GetChild(index).gameObject;
    }
    private void Start()
    {
        Init();
    }
    private void InstantiateCells()
    {
        Transform parent = this.transform;
        Vector2Int[] poses = tetramino.Poses;
        InstantiateCells(parent, offset, poses, color);
    }
    private static void InstantiateCells(Transform parent, float offset, Vector2Int[] poses, Color color)
    {
        LoopUtil.LoopAction((i) => SquareUtil.InstantiateAndSetUpSquare(parent, offset* (Vector2)poses[i], color),
            poses.Length);
    }
    #region modify center position
    public void SetCenterPosition(Vector2Int tetraminoCenterPos)
    {
        tetramino.SetCenterPosition(tetraminoCenterPos);
        UpdatePosition();
    }
    public void TranslateCenterPosition(Vector2Int offset)
    {
        Vector2Int newPositon = tetramino.centerPos + offset;
        SetCenterPosition(newPositon);
    }
    private void UpdatePosition()
    {
        transform.position = (Vector2)tetramino.centerPos;
    }
    #endregion
    #region rotation methods
    public void Rotate(int rotationCount)
    {
        tetramino.Rotate(rotationCount);
        UpdatePosesAfterRotation();
    }
    public void RotateClockwise()
    {
        tetramino.RotateClockwise();
        UpdatePosesAfterRotation();
    }
    public void RotateAntiClockwise()
    {
        tetramino.RotateAntiClockwise();
        UpdatePosesAfterRotation();
    }
    public void UpdatePosesAfterRotation()
    {
        Transform[] children = TransformUtil.GetChildren(transform);
        Vector2[] poses = VectorUtil.Multiply(tetramino.Poses, offset);
        TransformUtil.ApplyLocalPoses(children, poses);
    }
    #endregion
   
    #region init methods
    public void StartInit()
    {
        Init();
    }
    private void UndoInit()
    {
        Transform[] children = TransformUtil.GetChildren(transform);
        transform.DetachChildren();
        LoopUtil.LoopAction((i) => Destroy(children[i].gameObject), children.Length);
        inited = false;
    }
    public void RedoInit()
    {
        UndoInit();
        Init();
    }
    public void Init()
    {
        if (inited)
            return;
        Vector3 position = transform.position;
        Vector2Int tetraminoCenterPos = VectorUtil.V3_V2Int(position);
        Init(tetraminoCenterPos);
    }
    //public void Init(Vector2Int centerPos, int rotationCount)
    //{
    //    if (tetramino.rotationCount != 0)
    //    {
    //        Debug.LogError("trying to init, but tetramino data already rotated!");
    //    }
    //    tetramino.Rotate(rotationCount);
    //    Init();
    //}

    

    public void Init(Vector2Int tetraminoCenterPos)
    {
        if (inited)
            return;
        SetCenterPosition(tetraminoCenterPos);
        InstantiateCells();
        inited = true;
    }
    #endregion
    public static GameObject Instantiate(Tetramino.TetraminoType type, string name = "")
    {
        GameObject instance = new GameObject(name);
        TetraminoMono tetraminoMono = instance.AddComponent<TetraminoMono>();
        tetraminoMono.type = type;
        tetraminoMono.Init();
        return instance;
    }
    #region opacity & color stuff
    private float opacity = 1f;
    private bool delayedStartOpacity = false;
    public void SetOpacity(float opacity)
    {
        int childCount;
        SpriteRenderer[] spriteRenderers = 
            GetComponentUtil.GetComponentsInChildren<SpriteRenderer>(transform,out childCount);
        LoopUtil.LoopAction((i) => 
        spriteRenderers[i].color = 
        ColorUtil.ColorWithOpacity(spriteRenderers[i].color, opacity), childCount);
    }
    public void SetDelayedOpacity(float opacity)
    {
        this.opacity = opacity;
        delayedStartOpacity = true;
    }
    private void SetColor(Tetramino.TetraminoType type)
    {
        int childCount;
        SpriteRenderer[] spriteRenderers = 
            GetComponentUtil.GetComponentsInChildren<SpriteRenderer>(transform, out childCount);
        LoopUtil.LoopAction((i) => spriteRenderers[i].color = TetraminoUtil.Color(type), childCount);
    }
    #endregion
}
