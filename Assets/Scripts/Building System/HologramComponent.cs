using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class HologramComponent
    : MonoBehaviour, Visibility
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Camera camera;
    [SerializeField] private PlayerStateComponent playerState;
    
    private Visibility spriteVisibility;
    private ActionExecution followMouse;
    
    private void Awake()
    {
        Movable body = new ComponentInObject<Movable>(
            gameObject,
            new NullMovable()
        ).Value();

        Placement hologramPlacement = new MouseCursorFollower(
            body,
            camera,
            new ZCoordinateWorld()
        );
        
        spriteVisibility = new SpriteVisibility(spriteRenderer);
        followMouse = new ConditionalExecution(
            new ConstantExecution(
                new SimpleActionCall(() =>
                    {
                        hologramPlacement.Place();
                    }
                )
            ),
            new IsIdentityCondition<State>(
                playerState, new BuildState()
            )
        );
    }

    private void Update()
    {
        followMouse.Execute();
    }

    // Proxy
    public void Show() {
        spriteVisibility.Show();
    }
    
    public void Hide() {
        spriteVisibility.Hide();
    }

    public Condition IsVisible()
        => spriteVisibility.IsVisible();
}