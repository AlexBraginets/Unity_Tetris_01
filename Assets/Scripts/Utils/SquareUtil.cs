using UnityEngine;

public static class SquareUtil
{
    public static GameObject InstantiateSquare(out SpriteRenderer spriteRenderer, string name)
    {
        GameObject go = new GameObject(name);
        spriteRenderer = go.AddComponent<SpriteRenderer>();
        return go;
    }

    public static GameObject InstantiateAndSetUpSquare(Transform parent, float offset, Vector2 pos, Color color)
    {
        float x = pos.x;
        float y = pos.y;

        SpriteRenderer spriteRenderer;
        GameObject go = InstantiateSquare(out spriteRenderer, $"[{x}, {y}]");

        spriteRenderer.sprite = SpriteManager.SquareSprite;
        spriteRenderer.color = color;

        go.transform.parent = parent;
        go.transform.localPosition = new Vector2(x, y) * offset;

        return go;
    }
}
