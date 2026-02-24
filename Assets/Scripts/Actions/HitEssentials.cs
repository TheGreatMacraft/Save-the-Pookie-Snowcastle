using UnityEngine;

[System.Serializable]
public class HitEssentials : MonoBehaviour
{
    public int damageAmount;
    public float knockbackStrength;
    public string affectedObjectsTag;

    public void CopyFrom(HitEssentials hitEssentials)
    {
        damageAmount = hitEssentials.damageAmount;
        knockbackStrength = hitEssentials.knockbackStrength;
        affectedObjectsTag = hitEssentials.affectedObjectsTag;
    }
}