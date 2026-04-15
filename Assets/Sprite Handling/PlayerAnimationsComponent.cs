using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]

public sealed class PlayerAnimationsComponent
    : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteSheet;

    private AngledSpriteGroup idleGroup;
    private AngledSpriteGroup runningGroup;
    private AngledSpriteGroup rollingGroup;
    
    private void Awake()
    {
        GameObject parent = new ParentOfGameObject(gameObject).Parent();
        
        Location thisEntityLocation = new ComponentInObject<Location>(
            parent,
            new NullLocation()
            ).Value();

        TargetLocationSource targetLocationSource = new ComponentInObject<TargetLocationSource>(
            parent,
            new NullTargetLocationSource()
            ).Value();

        Vector facingDirection = new Vector(
            new PointToPointVectorDefinition(
                thisEntityLocation,
                targetLocationSource
            )
        );

        SpriteRenderer spriteRenderer = new ComponentInObject<SpriteRenderer>(
            gameObject,
            null
        ).Value();

        PlayerAnimationFrames playerAnimationFrames 
            = new PlayerAnimationFrames(spriteSheet);

        idleGroup = new AngledSpriteGroup(
            playerAnimationFrames.Idle(),
            spriteRenderer,
            facingDirection
        );
        
        runningGroup = new AngledSpriteGroup(
            playerAnimationFrames.Running(),
            spriteRenderer,
            facingDirection
        );

        rollingGroup = new AngledSpriteGroup(
            playerAnimationFrames.Rolling(),
            spriteRenderer,
            facingDirection
        );
    }

    public void SetIdleFrame(int frameIndex)
    {
        idleGroup.SetSprite(frameIndex);
    }

    public void SetRunFrame(int frameIndex)
    {
        runningGroup.SetSprite(frameIndex);
    }
    
    public void SetRollFrame(int frameIndex)
    {
        rollingGroup.SetSprite(frameIndex);
    }
}