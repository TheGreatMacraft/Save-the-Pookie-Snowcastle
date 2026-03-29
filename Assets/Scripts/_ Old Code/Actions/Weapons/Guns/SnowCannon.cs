
public class SnowCannon : GunActionsBase
{
    protected override void UpdateActionNameList()
    {
        base.UpdateActionNameList();
        
        actionNames.Add("Ability");
    }

    public void Ability()
    {
        foreach (var el in projectilesShot)
        {
            el.GetComponent<Snowball>().isDestroyedByAbility = true;
            Destroy(el);
        }
    }
}