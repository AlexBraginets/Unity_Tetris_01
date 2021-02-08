
using UnityEngine;

public class WallBlock : IBlock
{
    public WallBlock(BlockType blockType, GameObject gameObject = null)
    {
        this.blockType = blockType;
        if(blockType == BlockType.Unspecified && gameObject == null)
        {
            Debug.LogError("Cannot assing null to gameObject when type is BlockType.Unspecified!");
        }
        this.gameObject = gameObject;
    }

    public BlockType blockType { get; set; }
    public GameObject gameObject { get; set; }
}
