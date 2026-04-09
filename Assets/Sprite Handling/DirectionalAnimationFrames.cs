using UnityEngine;

public sealed class DirectionalAnimationFrames
{
    private readonly Sprite[] front;
    private readonly Sprite[] back;
    private readonly Sprite[] frontSide;
    private readonly Sprite[] backSide;


    public DirectionalAnimationFrames(
        Sprite[] front,
        Sprite[] back,
        Sprite[] frontSide,
        Sprite[] backSide
    )
    {
        this.front = front;
        this.back = back;
        this.frontSide = frontSide;
        this.backSide = backSide;
    }
    
    public Sprite FrontFrameAt(int index)
        => front[index];
    
    public Sprite BackFrameAt(int index)
        => back[index];
    
    public Sprite FrontSideFrameAt(int index)
        => frontSide[index];
    
    public Sprite BackSideFrameAt(int index)
        => backSide[index];
}