using System.Collections.Generic;
using UnityEngine;

public class ActionImpacts : Impact
{
    private readonly List<Impact> allImpacts;


    public ActionImpacts(List<Impact> allImpacts)
    {
        this.allImpacts = allImpacts;
    }


    public void ApplyOn(GameObject targetGameObject)
    {
        foreach (Impact impact in allImpacts)
        {                                                                                                         
            impact.ApplyOn(targetGameObject);
        }
    }
}