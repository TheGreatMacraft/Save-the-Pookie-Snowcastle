using UnityEngine;
[DisallowMultipleComponent]

public sealed class PlayerComponent : 
    MonoBehaviour,
    TargetLocation
{
    [SerializeField] private Camera camera;
    
    private Location mouseWorldLocation;
    private Target mouseWorld;

    private Condition trueCondition = new TrueCondition();
    
    
    private void Awake()
    {
        mouseWorldLocation = new MouseCursorWORLDPosition(
            camera
        );
    }
    
    
    public Vector3 Coordinates()
        => mouseWorldLocation.Coordinates();

    public Condition IsTargetFound() => trueCondition;
}