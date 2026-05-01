using System;
using UnityEngine;

public sealed class AngledSpriteGroup
{
    private readonly DirectionalAnimationFrames directionalFrames;
    
    private readonly SpriteRenderer spriteRenderer;
    private readonly Vector facingDirection;
    private readonly Orientation spriteOrientation;

    
    public AngledSpriteGroup(
        DirectionalAnimationFrames directionalFrames,
        SpriteRenderer spriteRenderer,
        Vector facingDirection
        )
        : this(
            directionalFrames,
            spriteRenderer,
            facingDirection, 
            new SpriteXOrientation(spriteRenderer, facingDirection)
            ) {}
    
    private AngledSpriteGroup(
        DirectionalAnimationFrames directionalFrames,
        SpriteRenderer spriteRenderer,
        Vector facingDirection,
        Orientation spriteOrientation
    )
    {
        this.directionalFrames =  directionalFrames;
        this.spriteRenderer = spriteRenderer;
        this.facingDirection = facingDirection;
        this.spriteOrientation = spriteOrientation;
    }

    public void SetSprite(int frameIndex)
    {
        spriteOrientation.Orient();
        float angle =  facingDirection.AngleInDegrees();
        
        Sprite newPick;
        
        if (Mathf.Abs(angle) > 90f)
            angle += (angle > 0)
                ? -2 * (Math.Abs(angle) - 90)
                : 2 * (Math.Abs(angle) - 90);
        
        if (angle >= -90f && angle < -60f)
            newPick = directionalFrames.FrontFrameAt(frameIndex);
        else if (angle >= -60f && angle < 0f)
            newPick = directionalFrames.FrontSideFrameAt(frameIndex);
        else if (angle >= 0f && angle <= 60f)
            newPick = directionalFrames.BackSideFrameAt(frameIndex);
        else
            newPick = directionalFrames.BackFrameAt(frameIndex);
        
        
        spriteRenderer.sprite = newPick;
    }
}