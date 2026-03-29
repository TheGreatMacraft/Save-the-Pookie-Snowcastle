using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]

public class CameraComponent : 
    MonoBehaviour
{
    [SerializeField] private float fraction;
    [SerializeField] private PhysicalBodyComponent playerBodyComponent;
    
    private Placement cameraPlacement;
    
    
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
            new MouseCursorSCREENPosition()
        );

        cameraPlacement = new SimplePlacement(
            cameraBody,
            new CameraDestination(
                new PointToPointVectorDefinition(
                    playerLocation,
                    mouseLocation
                    ),
                fraction
                )
            );
    }

    private void Update()
    {
        cameraPlacement.Place();
    }
}