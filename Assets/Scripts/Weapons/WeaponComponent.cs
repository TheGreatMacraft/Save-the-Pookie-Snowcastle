using UnityEngine;

public abstract class WeaponComponent
    : MonoBehaviour, Weapon
{
    [Header("Target")]
    [SerializeField] protected string targetTag;
    
    [Header("Camera Shake")]
    [SerializeField] private bool shakeCamera;
    [SerializeField] protected CameraShakeComponent cameraShake;
    [SerializeField] protected float shakeMagnitude;
    [SerializeField] protected float shakeDuration;
    
    
    protected Clock coroutineClock;

    protected ActionExecution defaultAttack = new NullActionExecution();
    protected ActionExecution supportAction = new NullActionExecution();
    protected ActionExecution heavyAttack = new NullActionExecution();
    protected ActionExecution abilityAction = new NullActionExecution();
    

    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);
    }
    
    
    public ActionExecution DefaultAttack() => defaultAttack;
    public ActionExecution SupportAction() => supportAction;
    public ActionExecution HeavyAttack() => heavyAttack;
    public ActionExecution Ability() => abilityAction;
}