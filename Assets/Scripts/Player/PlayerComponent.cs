using UnityEngine;
[DisallowMultipleComponent]

public sealed class PlayerComponent : 
    MonoBehaviour,
    TargetLocationSource
{
    [SerializeField] private Camera camera;
    
    private Location mouseWorldLocation;
    private Target mouseWorld;
    
    
    private void Awake()
    {
        mouseWorldLocation = new MouseCursorWORLDPosition(
            camera
        );
    }
    
    
    public Vector3 Coordinates()
        => mouseWorldLocation.Coordinates();
}