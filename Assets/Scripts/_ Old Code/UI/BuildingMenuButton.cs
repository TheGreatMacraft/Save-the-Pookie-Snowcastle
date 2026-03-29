using UnityEngine;

public class BuildingMenuButton : OnButtonClickBase<BuildingMenuButton>
{
    public GameObject objectToBuildPrefab;
    public GameObject objectHologramPrefab;

    public override void OnClickFunction()
    {
        BuildingDefenses.instance.SetDefense(objectToBuildPrefab, objectHologramPrefab);
    }
}