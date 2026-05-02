using UnityEngine;

public abstract class WeaponComponent
    : MonoBehaviour, Weapon
{
    [Header("Target")]
    [SerializeField] protected string targetTag;
    
    [Header("Camera Shake")]
    [SerializeField] private CameraShakeComponent cameraShakeComponent;
    [SerializeField] protected float shakeMagnitude;
    [SerializeField] protected float shakeDuration;
    
    
    private ActionExecution nullAction = new NullActionExecution();
    
    private Clock coroutineClock;
    private CameraShake cameraShake;


    protected Clock CoroutineClock()
        => coroutineClock ??=
            new CoroutineClock(this);

    protected CameraShake CameraShake()
        => cameraShake ??=
            (CameraShake)cameraShakeComponent ?? new NullCameraShake();
    
    public virtual ActionExecution DefaultAttack() => nullAction;
    public virtual ActionExecution SupportAction() => nullAction;
    public virtual ActionExecution HeavyAttack() => nullAction;
    public virtual ActionExecution Ability() => nullAction;
}