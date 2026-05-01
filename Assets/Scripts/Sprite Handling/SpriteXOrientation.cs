using UnityEngine;

public sealed class SpriteXOrientation
    : Orientation
{
    private readonly SpriteRenderer spriteRenderer;
    private readonly Vector directionalVector;


    public SpriteXOrientation(
        SpriteRenderer spriteRenderer,
        Vector directionalVector
        )
    {
        this.spriteRenderer = spriteRenderer;
        this.directionalVector = directionalVector;
    }
    
    
    public void Orient()
    {
        spriteRenderer.flipX 
            = Mathf.Abs(directionalVector.AngleInDegrees()) > 90f;
    }
}