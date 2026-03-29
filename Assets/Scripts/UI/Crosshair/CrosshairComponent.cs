using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class CrosshairComponent : 
    MonoBehaviour
{
    private Placement crosshairPlacement;


    private void Awake()
    {
        new MouseCursorVisibility().Hide();

        Movable movableCrosshair = new ComponentInObject<Movable>(
            gameObject,
            new NullMovable()
        ).Value();
        
        crosshairPlacement = new SimplePlacement(
            movableCrosshair,
            new MouseCursorSCREENPosition()
            );
    }
    
    private void Update()
    {
        crosshairPlacement.Place();
    }
}