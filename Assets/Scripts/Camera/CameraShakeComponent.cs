using UnityEngine;
using Random = UnityEngine.Random;
[DisallowMultipleComponent]

public class CameraShakeComponent
    : MonoBehaviour, CameraShake, Offset
{
    [SerializeField] private AnimationCurve curve;
    
    private PhysicalBody cameraBody;
    private Clock coroutineClock;
    
    private Vector3 currentOffset = Vector3.zero;

    public Vector3 Coordinates()
        => currentOffset;
    

    public void Shake(float magnitude, float duration)
    {
        coroutineClock.DoUntil((progress) =>
            {
                float strength = curve.Evaluate(progress)/10 * magnitude;
                currentOffset = Random.insideUnitSphere * strength;
            },
            duration,
            ()=> currentOffset = Vector3.zero
        );
    }
    

    private void Awake()
    {
        coroutineClock = new CoroutineClock(this);
    }
}