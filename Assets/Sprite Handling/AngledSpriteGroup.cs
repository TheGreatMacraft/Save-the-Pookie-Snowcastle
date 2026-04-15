using System;
using UnityEngine;

public sealed class AngledSpriteGroup
{
    private readonly DirectionalAnimationFrames directionalFrames;
    
    private readonly SpriteRenderer spriteRenderer;
    private readonly Vector facingDirection;

    public AngledSpriteGroup(
        DirectionalAnimationFrames directionalFrames,
        SpriteRenderer spriteRenderer,
        Vector facingDirection
        )
    {
        this.directionalFrames =  directionalFrames;
        this.spriteRenderer = spriteRenderer;
        this.facingDirection = facingDirection;
    }

    public void SetSprite(int frameIndex)
    {
        float angle =  facingDirection.AngleInDegrees();
        Sprite newPick;
        
        
        spriteRenderer.flipX = Mathf.Abs(angle) > 90f;
        
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