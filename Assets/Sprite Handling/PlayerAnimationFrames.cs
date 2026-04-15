using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAnimationFrames
{
    private readonly List<Sprite> spriteSheet;

    private readonly List<DirectionalAnimationFrames> idle = new(1);
    private readonly List<DirectionalAnimationFrames> running = new(1);
    private readonly List<DirectionalAnimationFrames> rolling = new(1);


    public PlayerAnimationFrames(Sprite[] spriteSheet)
        : this(new List<Sprite>(spriteSheet)) {}

    private PlayerAnimationFrames(List<Sprite> spriteSheet)
    {
        this.spriteSheet = spriteSheet;
    }

    
    public DirectionalAnimationFrames Idle()
    {
        if (idle.Count == 0)
        {
            idle.Add(new DirectionalAnimationFrames(
                spriteSheet.GetRange(0,4).ToArray(),
                spriteSheet.GetRange(23,4).ToArray(),
                spriteSheet.GetRange(41,4).ToArray(),
                spriteSheet.GetRange(59,4).ToArray()
            ));
        }
        
        return idle[0];
    }

    public DirectionalAnimationFrames Running()
    {
        if (running.Count == 0)
        {
            running.Add(new DirectionalAnimationFrames(
                spriteSheet.GetRange(4,8).ToArray(),
                spriteSheet.GetRange(27,8).ToArray(),
                spriteSheet.GetRange(45,8).ToArray(),
                spriteSheet.GetRange(63,8).ToArray()
            ));
        }
        
        return running[0];
    }
    
    public DirectionalAnimationFrames Rolling()
    {
        if (rolling.Count == 0)
        {
            rolling.Add(new DirectionalAnimationFrames(
                spriteSheet.GetRange(12,6).ToArray(),
                spriteSheet.GetRange(35,6).ToArray(),
                spriteSheet.GetRange(53,6).ToArray(),
                spriteSheet.GetRange(71,6).ToArray()
            ));
        }
        
        return rolling[0];
    }
}