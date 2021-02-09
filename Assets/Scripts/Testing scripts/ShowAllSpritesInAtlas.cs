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
            string name = sprite.name;
            Debug.Log(name);
            
            SpriteRenderer spriteRenderer;
            GameObject go = SquareUtil.InstantiateSquare(out spriteRenderer, name);

            spriteRenderer.sprite = sprite;

            go.transform.parent = transform;
            go.transform.localPosition = GridUtil.PositionFromIndex(spriteIndex, gridWidth);
            spriteIndex++;
        }
    }
   

}
