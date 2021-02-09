using UnityEngine;

public class LogTetramino : MonoBehaviour
{
    [SerializeField] TetraminoMono tetraminoMono;
   [ContextMenu("Log")]
   public void Log()
    {
        Debug.Log(tetraminoMono.tetramino);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Log();
        }
    }
    private void OnMouseDown()
    {
        Log();
    }
}
