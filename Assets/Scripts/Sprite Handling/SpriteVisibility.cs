using UnityEngine;

public sealed class SpriteVisibility
    : Visibility
{
    private readonly SpriteRenderer spriteRenderer;
    
    private Condition isVisible;


    public SpriteVisibility(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }


    public void Hide() => spriteRenderer.enabled = false;
    
    public void Show() => spriteRenderer.enabled = true;

    public Condition IsVisible() => isVisible ??= new IsTrue(()  => spriteRenderer.enabled);
}