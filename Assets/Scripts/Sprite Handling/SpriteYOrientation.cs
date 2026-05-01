using UnityEngine;

public sealed class SpriteYOrientation
    : Orientation
{
    private readonly SpriteRenderer spriteRenderer;
    private readonly Vector directionalVector;


    public SpriteYOrientation(
        SpriteRenderer spriteRenderer,
        Vector directionalVector
    )
    {
        this.spriteRenderer = spriteRenderer;
        this.directionalVector = directionalVector;
    }
    
    
    public void Orient()
    {
        spriteRenderer.flipY 
            = Mathf.Abs(directionalVector.AngleInDegrees()) > 90f;
    }
}