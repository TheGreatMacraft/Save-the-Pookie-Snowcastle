using UnityEngine;

public sealed class SpriteVisibility
    : Visibility
{
    private SpriteRenderer spriteRenderer;


    public SpriteVisibility(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }


    public void Hide()
    {
        spriteRenderer.enabled = false;
    }
    
    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public bool IsMet()
        => spriteRenderer.enabled;
}