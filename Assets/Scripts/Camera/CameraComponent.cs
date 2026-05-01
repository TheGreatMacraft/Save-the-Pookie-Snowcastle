using UnityEngine;
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public class CameraComponent : 
    MonoBehaviour
{
    [Header("Mouse Cursor Follower")]
    [SerializeField] private float fraction;
    [SerializeField] private PhysicalBodyComponent playerBodyComponent;
    
    private Placement cameraPlacement;
    private Offset shakeOffset;
    
    
    private void Awake()
    {
        Camera camera = new ComponentInObject<Camera>(
            gameObject,
            null
        ).Value();
        
        Movable cameraBody = new ComponentInObject<Movable>(
            gameObject,
            new NullMovable()
        ).Value();
        
        Location playerLocation = playerBodyComponent;
        Location mouseLocation = new ScreenPositionAsWorldPosition(
            camera,
            new MouseCursorSCREENPosition(new ZCoordinateUI())
        );

        shakeOffset = new ComponentInObject<Offset>(
            gameObject,
            new NullOffset()
        ).Value();

        cameraPlacement = new OffSetPlacement(
            cameraBody,
            new CameraDestination(
                new PointToPointVectorDefinition(
                    playerLocation,
                    mouseLocation
                    ),
                fraction
                ),
            new SimpleReadOnlyCollection<Offset>(
                shakeOffset
                )
            );
    }

    private void Update()
    {
        cameraPlacement.Place();
    }
}