using UnityEngine;

public class MunitionBase : MonoBehaviour
{
    public int damageAmount;
    
    public float knockbackStrength;
    public float knockbackDuration;
    
    public string targetTag;

    public void CopyFrom(MunitionBase munitionBase)
    {
        damageAmount = munitionBase.damageAmount;
        knockbackStrength = munitionBase.knockbackStrength;
        knockbackDuration = munitionBase.knockbackDuration;
        targetTag = munitionBase.targetTag;
    }
}