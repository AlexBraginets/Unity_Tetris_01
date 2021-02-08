using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteManager
{
    private static string atlasName = "Sprites/SkardFlatAtlas";
    public static Sprite SquareSprite
    {
        get
        {
            if(squareSprite == null)
            {
                squareSprite = Resources.LoadAll<Sprite>(atlasName)[0];
            }
            return squareSprite;
        }
    }
    private static Sprite squareSprite = null;
}
