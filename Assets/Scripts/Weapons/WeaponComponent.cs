using UnityEngine;

public abstract class WeaponComponent
    : MonoBehaviour, Weapon
{
    [Header("Target")]
    [SerializeField] protected string targetTag;
    
    [Header("Camera Shake")]
    [SerializeField] protected bool shakeCamera;
    [SerializeField] protected CameraShakeComponent cameraShake;
    [SerializeField] protected float shakeMagnitude;
    [SerializeField] protected float shakeDuration;
    
    
    private ActionExecution nullAction = new NullActionExecution();
    private Clock coroutineClock;


    protected Clock CoroutineClock()
        => coroutineClock ??=
            new CoroutineClock(this);
    
    public virtual ActionExecution DefaultAttack() => nullAction;
    public virtual ActionExecution SupportAction() => nullAction;
    public virtual ActionExecution HeavyAttack() => nullAction;
    public virtual ActionExecution Ability() => nullAction;
}