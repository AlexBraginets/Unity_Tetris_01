using UnityEngine;

public static class SquareUtil
{
    /// <summary>
    /// Creates gameObject with name name and adds SpriteRenderer
    /// </summary>
    /// <param name="spriteRenderer">reference to the newly added SpriteRenderer</param>
    /// <param name="name">name of gameObject</param>
    /// <returns>newly created gameObject with SpriteRenderer</returns>
    public static GameObject InstantiateSquare(out SpriteRenderer spriteRenderer, string name)
    {
        GameObject go = new GameObject(name);
        spriteRenderer = go.AddComponent<SpriteRenderer>();
        return go;
    }
    /// <summary>
    /// Creates a gameObject, assigns parent, adds SpriteRenderer to it,
    /// assigns Square sprite, assigns color, sets local position, returns reference to the gameObject
    /// </summary>
    /// <param name="parent">parent of the newly created gameObject</param>
    /// <param name="pos">local position of the newly created gameObject</param>
    /// <param name="color">color of the square sprite</param>
    /// <returns></returns>
    public static GameObject InstantiateAndSetUpSquare(Transform parent, Vector2 localPos, Color color)
    {
        float x = localPos.x;
        float y = localPos.y;

        SpriteRenderer spriteRenderer;
        GameObject go = InstantiateSquare(out spriteRenderer, $"[{x}, {y}]");

        spriteRenderer.sprite = SpriteManager.SquareSprite;
        spriteRenderer.color = color;

        go.transform.parent = parent;
        go.transform.localPosition = localPos;

        return go;
    }
}
