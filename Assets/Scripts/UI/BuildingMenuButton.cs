using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuButton :  OnButtonClickBase<BuildingMenuButton>
{
    public GameObject objectToBuildPrefab;
    public GameObject objectHologramPrefab;

    public override void OnClickFunction()
    {
        BuildingDefenses.instance.SetDefense(objectToBuildPrefab, objectHologramPrefab);
    }
}
