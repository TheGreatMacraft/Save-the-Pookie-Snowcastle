using UnityEngine;

[System.Serializable]
public class HitEssentials : MonoBehaviour
{
    public int damageAmount;
    
    public float knockbackStrength;
    
    public string targetTag;

    public void CopyFrom(HitEssentials hitEssentials)
    {
        damageAmount = hitEssentials.damageAmount;
        knockbackStrength = hitEssentials.knockbackStrength;
        targetTag = hitEssentials.targetTag;
    }
}