
public class SnowCannon : GunActionsBase
{
    public void Ability()
    {
        foreach (var el in projectilesShot)
        {
            el.GetComponent<Snowball>().isDestroyedByAbility = true;
            Destroy(el);
        }
    }

    protected override void RegisterActions()
    {
        base.RegisterActions();
        
        actions["Ability"] = Ability;
    }
}