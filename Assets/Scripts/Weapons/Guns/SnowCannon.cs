using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCannon : GunBaseClass
{
    public override void Ability()
    {
        foreach (var el in projectilesShot)
        {
            el.GetComponent<HitSnowball>().isDestroyedByAbility = true;
            Destroy(el);
        }
    }
}
