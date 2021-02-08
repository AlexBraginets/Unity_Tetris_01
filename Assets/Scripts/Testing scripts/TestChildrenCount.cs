using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonNonUnityUtils;
using TransformUtilLibrary;

public class TestChildrenCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PrintChildrenCount();
        var children = TransformUtil.GetChildren(transform);
        LoopUtil.LoopAction(
            (i)=>
            {
                children[i].parent = null;
                Destroy(children[i].gameObject);
            }, children.Length);
        PrintChildrenCount();
        LoopUtil.LoopAction((i)=>new GameObject().transform.parent = transform, 1);
        PrintChildrenCount();

    }
    private int printIndex = 0;
    private void PrintChildrenCount()
    {
        Debug.Log($"{++printIndex}: " + transform.childCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
