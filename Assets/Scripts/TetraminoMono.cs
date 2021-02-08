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
    public float offset => Grid.offset;
    public Tetramino tetramino
    {
        get
        {
            if (_tetramino == null || _tetramino.type != type)
            {
                _tetramino = new Tetramino(type);
            }
            return _tetramino;
        }
    }
    public GameObject GetChildGameObject(int index)
    {
        if (index < 0 || index >= 4)
        {
            Debug.Log("Cannot get child with index less than 0 or more then three!");
        }
        return transform.GetChild(index).gameObject;
    }
    public void UploadNewTetraminoData(Tetramino.TetraminoType type)
    {
        _tetramino = null;
        this.type = type;
    }
    public Color color
    {
        get
        {
            return ColorUtil.ColorWithOpacity(TetraminoUtil.Color(type), opacity);
        }
    }
    public Tetramino _tetramino = null;
    bool inited = false;
    #region opacity & color stuff
    private float opacity = 1f;
    private bool delayedStartOpacity = false;
    public void SetOpacity(float opacity)
    {
        int childCount;
        SpriteRenderer[] spriteRenderers = GetSpriteRenderers(transform,out childCount);
        LoopUtil.LoopAction((i) => 
        spriteRenderers[i].color = 
        ColorUtil.ColorWithOpacity(spriteRenderers[i].color, opacity), childCount);
        #region old version
        //for(int i = 0; i < 4; i++)
        //{
        //    Transform child = transform.GetChild(i);
        //    SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
        //    spriteRenderer.color = ColorUtil.ColorWithOpacity(spriteRenderer.color, opacity);
        //}
        #endregion
    }
    public void SetDelayedOpacity(float opacity)
    {
        this.opacity = opacity;
        delayedStartOpacity = true;
    }
    private void SetColor(Tetramino.TetraminoType type)
    {
        int childCount;
        SpriteRenderer[] spriteRenderers = GetSpriteRenderers(transform, out childCount);
        #region old version
        //for (int i = 0; i < 4; i++)
        //{
        //    Transform child = transform.GetChild(i);
        //    SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
        //    spriteRenderer.color = TetraminoUtil.Color(type);
        //}
        #endregion
        LoopUtil.LoopAction((i) => spriteRenderers[i].color = TetraminoUtil.Color(type), childCount);
    }
    #endregion
   
    void Start()
    {
        Init();
    }

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
        inited = true;
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
    private void InstantiateCells()
    {
        Transform parent = transform;
        Vector2Int[] poses = tetramino.Poses;
        InstantiateCells(parent, offset, poses, color);
    }
    #region modify center position
    public void SetCenterPosition(Vector2Int tetraminoCenterPos)
    {
        transform.position = (Vector2)tetraminoCenterPos;
        tetramino.centerPos = tetraminoCenterPos;
    }
    public void TranslateCenterPosition(Vector2Int offset)
    {
        Vector2Int newPositon = tetramino.centerPos + offset;
        SetCenterPosition(newPositon);
    }
    #endregion
    #region rotation methods
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
    private static void InstantiateCells(Transform parent, float offset, Vector2Int[] poses, Color color)
    {
        LoopUtil.LoopAction((i) => SquareUtil.InstantiateAndSetUpSquare(parent, offset, poses[i], color),
            poses.Length);
    }
    private static SpriteRenderer[] GetSpriteRenderers(Transform transform, out int length)
    {
        Transform[] children = TransformUtil.GetChildren(transform);
        int childCount = children.Length;
        SpriteRenderer[] spriteRenderers =
            LoopUtil.LoopFunc<SpriteRenderer>((i) => children[i].GetComponent<SpriteRenderer>(), childCount);
        length = childCount;
        return spriteRenderers;
    }
    public static GameObject Instantiate(Tetramino.TetraminoType type, string name = "")
    {
        //Debug.Log("TetraminoMono.Instantiate");
        GameObject instance = new GameObject();
        TetraminoMono tetraminoMono = instance.AddComponent<TetraminoMono>();
        tetraminoMono.type = type;
        tetraminoMono.name = name;
        tetraminoMono.Init();
        return instance;
    }
}
