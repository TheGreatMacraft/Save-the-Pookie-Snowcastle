using System;
using System.Collections.Generic;

public class SnowCannon : GunBase
{
    public void Ability()
    {
        foreach (var el in projectilesShot)
        {
            el.GetComponent<Snowball>().isDestroyedByAbility = true;
            Destroy(el);
        }
    }

    protected override Dictionary<string, Action> WeaponActionFunctions()
    {
        var baseFunctions = base.WeaponActionFunctions();

        baseFunctions["Ability"] = Ability;
        
        return baseFunctions;
    }
}