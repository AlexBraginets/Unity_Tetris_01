using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAllSpritesInAtlas : MonoBehaviour
{
    public int gridWidth;
    public float offset;
    public string atlasName = "Sprites/SkardFlatAtlas";
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(atlasName);
        Debug.Log(sprites.Length);
        int spriteIndex = 0;
        foreach(Sprite sprite in sprites)
        {
            Debug.Log(sprite.name);

            GameObject go = new GameObject(sprite.name);
            go.transform.parent = transform;
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            go.transform.localPosition = PositionFromIndex(spriteIndex);
            spriteIndex++;
        }
    }
    Vector2 PositionFromIndex(int index)
    {
        int x = index % gridWidth;
        int y = -(index / gridWidth);
        return new Vector2(x, y)*offset;
    }

    
}
